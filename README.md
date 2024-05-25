# GXPPlus
It's a modified version of gxp, what can I say?

Some features that are added or changed:
- Quite some properties are now capitalized, this is confusing for anyone that doesn't use this version though, sorry.
- Input now has a property for mouse position as a vector2, this makes working with the mouse in some cases a lot simpler.
- A different physics namespace, it works but not that well right now.
- A way to get assets from the static assets class, as long as files are categorized by "Sounds" and "Textures" in the "Assets" folder, 
the class can look for filenames in those folders so memorizing/copying long paths is not necessary.
- Scenes! The object works almost exactly the same as the game but you can add it as child to the game, switch to a different scene and back or just load another scene. 
It's easier than restarting the application.