import React from "react";
import {  CustomPIXIComponent } from "react-pixi-fiber";
import {Graphics} from "pixi.js";

interface Props {
    position: { x: number; y: number };
    color: number;
    width: number;
    height: number;
  }

export default CustomPIXIComponent<Graphics,Props>({

  customDisplayObject: () => new Graphics(),
  customApplyProps: function(instance, oldProps, newProps) {
   
    const { color, position, width, height, ...newPropsRest } = newProps;
    const { color: oldFColor, position: oldPosition, width: oldWidth, height: oldHeight, ...oldPropsRest } = oldProps;
    const {x,y} = position
    if (typeof oldProps !== "undefined") {
      instance.clear();
    }
    instance.beginFill(0xeee);
    instance.drawRect(x, y, width, height);
    instance.endFill();
  },
}, 'VerticalWall')