## FParticle

Sample built using F# + [Fable](http://fable.io/) that displays a swarm of particles attracted to the mouse pointer on an HTML5 canvas.

Inspired by a [JS implementation](http://www.playfuljs.com/particle-effects-are-easy/) made by Hunter Loftis.

## Demo

View a [live demo](http://htmlpreview.github.io/?https://github.com/bradyjoslin/FParticle/blob/master/demo/index.html)

Screencast:

![Markdown](https://github.com/bradyjoslin/FParticle/raw/master/screenshot/particles.gif)



## Build and running the app

1. Install npm dependencies: `yarn install`
2. Install dotnet dependencies: `dotnet restore`
3. Start Fable server and Webpack dev server: `dotnet fable npm-run start`
4. In your browser, open: http://localhost:8080/

Any modification you do to the F# code will be reflected in the web page after saving.
