//тут будуть типи, які потрібно
import { Position } from "../api/api";

export enum Event {
  mouseover = "mouseover",
  mouseout = "mouseout",
}

export enum WallDirection {
  vertical = 0,
  horizontal = 1,
}

export enum Color {
  lightGray = "#fffafa",
  lavender = "#E6E6FA",
  white = "#FFFFFF",
  black = "#000000",
  blue = "#1565c0",
  lightBlue = "#90caf9",
}

export const Theme = {
  wallColor: Color.lightGray,
  cellColor: Color.lavender,
};
export type Player = {
  id: number;
  numOfWallLeft: number;
  stepSize: number;
};
export type Step = {
  wallsToSend: WallToSend[];
  walls: Position[];
  player0: {
    x: number;
    y: number;
    remainingWalls: number;
  };
  player1: {
    x: number;
    y: number;
    remainingWalls: number;
  };
  stepNumber: number;
};
export const isEven = (x: number): boolean => {
  return x % 2 === 0;
};

export interface HoveredWall {
  direction: WallDirection;
  first: Position;
  second: Position;
  third: Position;
}

export interface WallToSend {
  direction: number;
  position: Position;
}

export const initialHoverValue: boolean[][] = new Array(17)
  .fill(false)
  .map((_) => {
    return new Array(17).fill(false);
  });

export const initialStep = {
  player0: {
    x: 0,
    y: 8,
    remainingWalls: 10,
  },
  player1: {
    x: 16,
    y: 8,
    remainingWalls: 10,
  },
  walls: [],
  wallsToSend: [],
  stepNumber: 0,
};
