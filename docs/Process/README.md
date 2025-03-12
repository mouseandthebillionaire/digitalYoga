# Process Journal

## 03.11.25 | Dem? Oh.

Fully functioning version is up that fixes all (?) of the issues mentioned below.

[Digital Yoga Phone Build](https://mouseandthebillionaire.github.io/digitalYoga/Builds/v0/)

* Rewritten for numbers instead of letters (though I'm being slightly lazy and using a function to find the number based on the letters in the poseList text file, and it's probably worth it to just go in and create numerical comma seperated values for those poses)
* The buttons now have the rings as a child object. This makes it waaay easier to lay out the grid (and also allows for expanded fields if we want more buttons)
* Sounds are there but don't sound very good. Notes below on some thoughts on that.
* Don't need to worry about the background color since the app is expanding to fill the screen
* The first 5 poses are set, and after that it is random. Feels fine?
* It's in portrait now, and yes, it feels better
  
### Thoughts
* lower the volume and add some echo to the glockenspiel notes
* add variety to the "success" melodic line. I thought there were more versions of that. Need to go in and see, and if not, write some more
* When you pick up your hands and the success line is playing it abruptly stops. A nicer volume fade is needed here
* There should probably be a constant background track too, yes?
* Write some intro text
* It's a two minute countdown experience right now. Does that even make sense? Should there be a timer at all? Is there an "end" state, or can you just play for as long as you feel like playing.
* Similarly, is the black bar communicating "holding the pose"? Is there another and/or better way to do this?
* The way build work in the new version of Unity it just automatically overwrites what was there before? Need to figure out how to create new versions of this for posterity

## 03.10.25 | ReUP

The files have been updated and reuploaded to this new repo. Additionally, the current playable build is here: [v0](https://mouseandthebillionaire.github.io/digitalYoga/Builds/v0/). There are a _ton_ of issues with this one, but it's moving forward nicely. Not a lot of high-level design thoughts right now (I think it works pretty well?) but some functional changes:

- since this was originally built for keyboard input (haha. Remember that!?) it is still referencing every button/ring combo by a letter rather than its number, which is very confusing for debugging etc. Probably a good idea to rewrite this to reference the number. A ton of work perhaps, but will simplify things as we go.
- Speaking of: the button/rings are actually completely separate objects, and this is a nightmare for layout tweaks (and possibly for different screen resolutions) Job 1 might be to combine these into a single prefab.
- ALSO! The button letter-names aren't even being laid-out correctly which is messing with the requested poses.
- The sounds aren't coming through
- Can we get the html background to change at the same rate as the app background?
- It's a set progression right now, maybe change that? Or maybe not? Maybe who cares!
- Switch to portrait mode. Makes more sense as a yoga mat (and for phone)
- This also gives us more space for some introductory text (which I am loathe to write, but is probably welcome...)

More later. But this is a good place to start.
 
## 03.06.25 | A new life!

This project existed pre-MDM-focused workflow and so most of it is lost to the sands of time. But as I work to port it to a web-based experience I will document the process from today onward.

Onward!