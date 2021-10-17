import { Container, Stage } from "@inlet/react-pixi";
import { Box } from "@mui/system";
import React, { useState } from "react";
import { Color, HoveredWall, Step } from "../Utils";

import Cell from "./Cell";
import HorisontalWall from "./Wall/HorisontalWall";
import IntersectionWall from "./Wall/IntersectionWall";
import VerticalWall from "./Wall/VerticalWall";

interface Props {
  step: Step;
  wallColor: string;
  cellColor: string;
  isHover: boolean[][];
  mouseHover: (hoveredWall: HoveredWall) => void;
  mouseOut: () => void;
  putWall: (hoveredWall: HoveredWall) => void;
  makeMove: (position: { x: number; y: number }) => void;
}
const Field = ({
  step,
  wallColor,
  cellColor,
  mouseHover,
  isHover,
  mouseOut,
  putWall,
  makeMove,
}: Props) => {
  const selectWallColor = (
    isHover: boolean,
    step: Step,
    x: number,
    y: number
  ): string => {
    const isClick = step.walls.find((wall) => wall.x === x && wall.y === y)
      ? true
      : false;

    return isClick ? "#002884" : isHover ? Color.blue : Color.lightBlue;
  };

  const width = window.screen.availHeight * 0.8;
  const height = window.screen.availHeight * 0.8;
  const stageProps = {
    height,
    width,
    options: {
      backgroundAlpha: 1,
    },
  };

  return (
    <Box
      sx={{
        width: { width },
        height: { height },
      }}
    >
      {new Array(17).fill(1).map((_, y) => {
        return (
          <Box sx={{ display: "flex" }} key={y}>
            {new Array(17).fill(1).map((_, x) => {
              if (y % 2 === 0 && x % 2 === 0) {
                return (
                  <Cell
                    key={y * 17 + x}
                    step={step}
                    x={x}
                    y={y}
                    makeMove={makeMove}
                  />
                );
              } else if (x % 2 === 1 && y % 2 === 0) {
                return (
                  <VerticalWall
                    position={{ x, y }}
                    mouseHover={mouseHover}
                    mouseOut={mouseOut}
                    color={selectWallColor(isHover[y][x], step, x, y)}
                    putWall={putWall}
                    key={y * 17 + x}
                  />
                );
              } else if (y % 2 === 1 && x % 2 === 0) {
                return (
                  <HorisontalWall
                    position={{ x, y }}
                    mouseHover={mouseHover}
                    mouseOut={mouseOut}
                    color={selectWallColor(isHover[y][x], step, x, y)}
                    key={y * 17 + x}
                    putWall={putWall}
                  />
                );
              } else {
                return (
                  <IntersectionWall
                    position={{ x, y }}
                    color={selectWallColor(isHover[y][x], step, x, y)}
                    key={y * 17 + x}
                  />
                );
              }
            })}
          </Box>
        );
      })}
    </Box>
  );
};

export default Field;
