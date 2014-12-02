Configuration = 'Release'

COPYRIGHT = "Copyright 2013 Eugene Tolmachev, All rights reserved."
DESCRIPTION = 'Bitlore, minimalist validation library for DDD affectionados'
PRODUCT = 'Bitlore'

require 'albacore'
require 'albacore/tasks/versionizer'
require 'semver'
include FileTest

props = {
  :src => File.expand_path("src"),
  :out => File.expand_path("build_output"),
  :artifacts => File.expand_path("build_artifacts"),
  :projects => ["Bitlore"],
  :lib => File.expand_path("lib"),
  :keyfile => File.expand_path("Bitlore.snk")
}

desc "Cleans, compiles, unit tests, prepares examples, packages zip"
task :all => [:default, :package]

desc "**Default**, compiles and runs tests"
task :default => [:clean, :nuget_restore, :compile, :tests, :package]

desc "Prepares the working directory for a new build"
task :clean do
	FileUtils.rm_rf props[:out]
	waitfor { !exists?(props[:out]) }

	FileUtils.rm_rf props[:artifacts]
	waitfor { !exists?(props[:artifacts]) }

	Dir.mkdir props[:out]
	Dir.mkdir props[:artifacts]
end

desc "Cleans, versions, compiles the application and generates build_output/."
task :compile => [:versioning, :asmver, :nuget_restore, :build] do
	copyOutputFiles File.join(props[:src], "Bitlore/bin/Release"), "Bitlore.{dll,pdb,xml}", File.join(props[:out], 'net-portable')
end

desc 'create assembly info'
asmver :asmver => :versioning do |a|
  a.file_path = File.join(props[:src],'SolutionVersion.cs')
  a.attributes assembly_description: DESCRIPTION,
               assembly_configuration: Configuration,
               assembly_copyright: COPYRIGHT,
               assembly_version: ENV['FORMAL_VERSION'],
               assembly_file_version: ENV['FORMAL_VERSION'],
               assembly_informational_version: ENV['BUILD_VERSION']
end

build :build do |b|
  b.file = File.join(props[:src],'Bitlore.sln')
  b.prop 'Configuration', Configuration
end

desc "Runs unit tests"
test_runner :tests => [:compile] do  |t|
  t.exe = File.join(props[:src], 'packages','xunit.runners.1.9.2', 'tools', 'xunit.console.clr4.exe')
  t.add_parameter "/xml" 
  t.add_parameter "#{File.join(props[:artifacts], 'xunit-test-results.xml')}"
  t.files = FileList[File.join(props[:src], "Bitlore.Tests/bin/#{Configuration}", "Bitlore.Tests.dll")]
end

task :package => [:create_nuget]

desc "restores missing packages"
task :nuget_restore do 
  solution = File.join(props[:src],'Bitlore.sln')
  system 'src/.nuget/nuget.exe', 'restore', solution, clr_command: true
end

Albacore::Tasks::Versionizer.new :versioning

desc "Builds the nuget package"
nugets_pack :create_nuget => [:versioning, :compile] do |p|
  p.files   = FileList['src/**/*.{csproj,fsproj,nuspec}'].
    exclude(/Tests/)
  p.out     = 'build_artifacts'
  p.exe     = 'src/.nuget/nuget.exe'
  p.with_metadata do |m|
    m.id          = 'Bitlore'
    m.title       = PRODUCT
    m.description = DESCRIPTION
    m.authors     = COPYRIGHT
    m.project_url = 'http://github.com/et1975/Bitlore'
    m.tags        = ['DDD', 'Validation']
    m.version     = ENV['NUGET_VERSION']
  end
  p.with_package do |p|
	add_assemblies props[:out], '*.{dll,xml}', p 
  end
end

#task :ensure_nuget_key do
#  raise 'missing env NUGET_KEY value' unless ENV['NUGET_KEY']
#end

#Albacore::Tasks::Release.new :release,
#                             pkg_dir: 'build/pkg',
#                             depend_on: [:create_nugets, :ensure_nuget_key],
#                             nuget_exe: 'packages/NuGet.CommandLine/tools/NuGet.exe',
#                             api_key: ENV['NUGET_KEY']

def copyOutputFiles(fromDir, filePattern, outDir)
	FileUtils.mkdir_p outDir unless exists?(outDir)
	Dir.glob(File.join(fromDir, filePattern)){|file|
		copy(file, outDir) if File.file?(file)
	}
end

def add_assemblies stage, what_dlls, nuspec
  [['portable-win+net40+sl40+wp', 'net-portable']].each{|fw|
    takeFrom = File.join(stage, fw[1], what_dlls)
    Dir.glob(takeFrom).each do |f|
      nuspec.add_file f.gsub("/", "\\"), "lib\\#{fw[0]}"
    end
  }
end

def waitfor(&block)
	checks = 0

	until block.call || checks >10
		sleep 0.5
		checks += 1
	end

	raise 'Waitfor timeout expired. Make sure that you aren\'t running something from the build output folders, or that you have browsed to it through Explorer.' if checks > 10
end
