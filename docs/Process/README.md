# Process Journal

## 03.10.25 | ReUP

The files have been updated and reuploaded to this new repo. Additionally, the current playable build is here: [v0](https://mouseandthebillionaire.github.io/digitalYoga/Builds/v0/). There are a _ton_ of issues with this one, but it's moving forward nicely. Not a lot of high-level design thoughts right now (I think it works pretty well?) but some functional changes:

- since this was originally built for keyboard input (haha. Remember that!?) it is still referencing every button/ring combo by a letter rather than its number, which is very confusing for debugging etc. Probably a good idea to rewrite this to reference the number. A ton of work perhaps, but will simplify things as we go.
- Speaking of: the button/rings are actually completely separate objects, and this is a nightmare for layout tweaks (and possibly for different screen resolutions) Job 1 might be to combine these into a single prefab.
- Can we get the html background to change at the same rate as the app background?
- It's a set progression right now, maybe change that? Or maybe not? Maybe who cares!
- Switch to portrait mode. Makes more sense as a yoga mat
- This also gives us more space for some introductory text (which I am loathe to write, but is probably welcome...)

More later. But this is a good place to start.
 
## 03.06.25 | A new life!

This project existed pre-MDM-focused workflow and so most of it is lost to the sands of time. But as I work to port it to a web-based experience I will document the process from today onward.

Onward!