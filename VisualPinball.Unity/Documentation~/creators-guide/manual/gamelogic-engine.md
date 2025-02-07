---
description: VPE's gamelogic engine is the code driving your game's logic.
---
# Gamelogic Engine

When playing a pinball game, some part of the table is driving the gameplay, i.e. deciding when to flip a coil, turn on a light, show something on the DMD, and so on. In VPE, we call this the *Gamelogic Engine*.

The gamelogic engine is purely gameplay driven. It gets input from switches, computes what will happen next, and updates the hardware components of the table. It does *not* handle game mechanics, which are about simulating the hardware *behavior* of the table - it just toggles it.

Classic examples of gamelogic engines are [MPF](../../plugins/mpf/index.md) and [PinMAME](https://github.com/vpinball/pinmame).

> [!note]
> Let's take a spinning wheel on the playfield as an example. The game*logic* engine's job is to know when to turn it on and off. The game *mechanics* component of the spinning wheel is about rotating the actual playfield element with the right speed, acceleration, and handle ball collisions with a given friction.
>
> At the moment it's still unclear how VPE will deal with game mechanics. Initially, we will ship a bunch of game mechanics ready to use, and the future will tell how authors can create their own.

In Visual Pinball, the gamelogic engine is part of the table script, which in most cases uses VPM to drive the game. So a part of the table script is about piping data into VPM and handling its outputs (lamp changes, coil changes, and so on).

Since VPE defines a clear API (like a contract) between the table and the gamelogic engine, we can provide tools to make this easy for you. Currently, VPE provides:

- A [Switch Manager](~/creators-guide/editor/switch-manager.md)
- A [Lamp Manager](~/creators-guide/editor/lamp-manager.md)
- A [Coil Manager](~/creators-guide/editor/coil-manager.md)

These tools provide a graphical user interface where you can link playfield elements to the gamelogic engine and configure them. 

Ultimately, that means if your table uses an existing gamelogic engine like MPF or PinMAME, and the table doesn't contain any exotic game mechanics, that's all you need to do. You can set up your table without a single line of code!
