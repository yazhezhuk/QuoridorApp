import React from "react";
import {  CustomPIXIComponent } from "react-pixi-fiber";
import {Graphics} from "pixi.js";

interface Props {
    position: { x: number; y: number };
    color: number;
    radius: number;

  }

export default CustomPIXIComponent<Graphics,Props>({

  customDisplayObject: () => new Graphics(),
  customApplyProps: function(instance, oldProps, newProps) {
   
    const { color, position, radius, ...newPropsRest } = newProps;
    const { color: oldFColor, position: oldPosition, radius: oldRadius,...oldPropsRest } = oldProps;
    const {x,y} = position
    if (typeof oldProps !== "undefined") {
      instance.clear();
    }
    instance.beginFill(0xeee);
    instance.drawCircle(x, y, radius);
    instance.endFill();

  },
}, 'Player')