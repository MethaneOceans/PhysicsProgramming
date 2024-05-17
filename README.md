# Final Approach "Wizard with a laser!"
## Goal functionality
- Throw an object in the air
- Lock the object in place when te player chooses
- Rotate the object
- Hit the object with a laser and emit resulting lasers
- Hit a goal object with the laser using these mechanics

## Underlying functionality
- Ray intersection solution for the shape of the thrown object
- Kinematics for thrown objects
- Object responses for getting hit by laser (event based probably?)

## Other stuff
- If the laser should be sprite based then a new Sprite inheriting class has to be made that changes the uv coordinates so the laser sprite does not get stretched
- Sound
- Levels?
- Other objects?
- Maybe the "prism" should use actual refraction or just emit lasers perpendicular to the edges

## TODO
- [ ] - Raycasting
- [ ] - Drawing the ray on the screen
- [ ] - Kinematics update method (just euler)
- [ ] - Win state trigger if goals are all hit
- [ ] - Create "Prism" object
- [ ] - Shoot a new prism
- [ ] - Lock the prism in place
- [ ] - Rotate the prism
- [ ] - Cast new rays from prism object
- [x] - Aim catapult at mouse
