import React from "react";
import { CustomDisplayObject, CustomPIXIComponent } from "react-pixi-fiber";
import * as PIXI from "pixi.js";

interface Props {
  
}

const TYPE = "Cell";
export let behavior = {
  customDisplayObject: () => new PIXI.Graphics(),
};

export default CustomPIXIComponent(behavior, TYPE);
