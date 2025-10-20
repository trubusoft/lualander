Practicing Code Monkey's [Lua Lander](https://youtu.be/nGKd4yTP3M8?si=MLKIuNU28Sjii9XB).

Automatic WebGL deployment workflow on GitHub Pages is also available (see [pages.yaml](.github/workflows/pages.yaml)).

This repo has relatively organized and linear commit history, each PR representing unique section from the video.

Credit to Hugo for making this wonderful tutorial, and to [game-ci](https://game.ci/) for the great unity builder.

`code-monkey` branch contain the original tutorial as-is. 
While `main` branch contains self-opinionated-refactored version with such key points in mind:
- Avoid singleton when possible
- Each object must be a prefab (including Lander)
- Each object ideally has its own debug scene to easily test its behaviour, and isn't included in the main build.
- Call down, signal up
- Encapsulation (anything that process a class must be done on that class)
