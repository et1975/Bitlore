# Bitlore

Bitlore provides a simple way to implement flexible validation scenarios in .NET framework.

Bitlore is a natural fit for complex domain models: 
 combine Rules and Interpretations in Specifications to implement easy to follow and test scenarios. 

Small: 
 Only three basic concepts!
Fluff-free: 
 Use your programming language and your model to define the rules.
Extensible: 
 Implement Rule interface to encapsulate complexity, define your own specifications and interpretations.

## Sample of a spec
```
 Specifications.All(new Dictionary<Rule<int>,Func<Rule<int>,Interpretation>>
            {
                {Rules<int>.Predicate(x => x % 2 == 0), _ => Interpretations.Static("NOT_EVEN")},
                {Rules<int>.Predicate(x => x > 0), _ => Interpretations.Static("NOT_POSITIVE")}
            });
```

Defines 2 rules and combines them in the specification that can be used to perform a validation:
```
 Assert.True(new[] { "NOT_EVEN" }.SequenceEqual(spec.Test(3).Select(i => i.AsText())));
```

See the unit tests for complete details.


## Building Bitlore

 1. Clone the source down to your machine. 
   `git clone git://github.com/et1975/Bitlore.git`
 1. Ensure Ruby is installed. [RubyInstaller for Windows](http://rubyinstaller.org/)
 1. Ensure `git` is on your path. `git.exe` and `git.cmd` work equally well.
 1. **Ensure gems are installed**, run:

```
gem install albacore
gem install semver2
```

 4. Run `rake`
 
## Target frameworks
The library is portable and available for
* .Net >= 4.0
* Silverlight >= 4.0
* WP >= 7.5
* Windows Store 

## Credits
Build script has been adopted from Chris Patterson's (http://github.com/phatboyg) projects.
