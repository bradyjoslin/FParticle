module FParticle

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser

// Pull in Math.random from JS to use here in F#
[<Emit("Math.random()")>]
let random (): float = jsNative

type Mouse = {
    mutable x : float
    mutable y : float
}

type Particle = {
    x : float
    y : float
    xOld : float
    yOld : float
}

type Particles = {
    mutable list : Particle list
}

// Attracts the particles to a point on the screen
let attract x y particle =
    let dx = x - particle.x
    let dy = y - particle.y
    let distance = sqrt(dx * dx + dy * dy)
    { particle with 
        x = particle.x + (dx / distance)
        y = particle.y + (dy / distance)   
    }

// Vertlet integration https://en.wikipedia.org/wiki/Verlet_integration
let integrate particle =
    let damping = 0.999
    let velocityX = (particle.x - particle.xOld) * damping
    let velocityY = (particle.y - particle.yOld) * damping
    { particle with
        xOld = particle.x 
        yOld = particle.y
        x = particle.x + velocityX
        y = particle.y + velocityY
    }

// Draws particles on the screen
let draw (ctx : CanvasRenderingContext2D) particle =
    ctx.strokeStyle <- !^"#ffffff"
    ctx.lineWidth <- 2.
    ctx.beginPath()
    ctx.moveTo(particle.xOld, particle.yOld)
    ctx.lineTo(particle.x, particle.y)
    ctx.stroke()

// Get some information about the UI
let width = window.innerWidth
let height = window.innerHeight
let canvas = Browser.document.getElementsByTagName_canvas().[0]
canvas.width <- width
canvas.height <- height  

// Maintains state of the mouse pointer, initialized in the middle of screen
let mouse = {
    x = width * 0.5
    y = height * 0.5
}

let onMouseMove (e : MouseEvent) =
    mouse.x <- e.clientX
    mouse.y <- e.clientY
    null

document.addEventListener_mousemove(fun e -> onMouseMove(e))

// Maintains state of particles in a list
// Initializes containing particles with randomized x,y coordinates
let particles =
    { list =
        [ for _ in 1..500 do
            let xInit = random() * width
            let yInit = random() * height
            yield {
                x = xInit
                y = yInit
                xOld = xInit
                yOld = yInit
            }
        ]
    }

// Recursive function that attracts, integrates, and draws the particles
let rec main (dt:float) =
    window.requestAnimationFrame(FrameRequestCallback main) |> ignore
    let ctx = canvas.getContext_2d()
    ctx.clearRect(0., 0., width, height)

    particles.list <-
        particles.list
        |> List.map (attract mouse.x mouse.y >> integrate)

    particles.list |> List.iter (draw ctx)

main 0.