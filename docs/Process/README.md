# Process Journal

## 03.06.25 | A new life!

This project existed pre-MDM-focused workflow and so most of it is lost to the sands of time. But as I work to port it to a web-based experience I will document the process from today onward.

Onward!

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
* The way build work in the new version of Unity it just automatically overwrites what was there before? Need to figure out how to create new versions of this for posterity.

## 03.15.25 | Make it More Yoga

I want to try something different here, and begin the design day by writing (rather than waiting entirely to the end). Yesterday met with PJB and discussed the state of Digital Yoga. The general gist is "it's fine, but how can it be more yoga?" Some thoughts that came up during and because of that discussion:

### Thoughts
* Obviously vocal direction is an integral part in traditional yoga practice, so that could easily fit here. ("(FWIW I love a breathy woman’s voice talking me through the poses)" - Anonymous). The fear here is going too far into parody perhaps? It might be a fine line? But it also might be a compelling addition. Something to definitely explore.
* Much of the action of yoga is directing the focus to what is happening in the body (tension in joints, releasing of muscles, deepening of breath, etc) and that is entirely absent here. How can we build this in? Voice, again obviously, is the easiest solution, but could we use on-screen text prompts or some other way to signal this attention?
* Yoga is about movement and flow as much as it is about a given "pose." The current version does nothing to encourage that. This realization makes me see just _how_ off it is. It's fairly clear that the grid-based solution that I've been working with is not the way to go here. In fact, it's wholly unnecessary. Rather, it would be better to have the dots show up at given locations, the user places their fingers on the dots, and then the active dots get moved to new locations as well as adding additional dots. Unfortunately this probably means a fairly serious rebuild, but it's all in the name of being honest to the design! (Put that in your MDM dream journal!)
* Note: The really compelling thing for me here is the introduction of failure. Currently, you get to choose which fingers go where, but with this movement and addition mechanic you will be confronted with the possibility that whatever configuration you thought was correct may not let you transition smoothly to the next pose. This feels in line with the experience of a yoga class where you are focusing on a pose, they ask you to do something, and you immediately look up to see what others are doing because it in no way feels like your body can do what is being asked of it...

### Goals and Prototypes
* Explore the possibility of moving poses and adding digits to currently active poses. Does this feel more "yoga"...?
* With this, build in the _transition_ as a step between the two poses. "And now slowly slide your left finger up to... Pose Y"
* An external _process_ goal to reflect on these design moves directly at the end of the work session (perhaps even as an amendment to this journal entry?)