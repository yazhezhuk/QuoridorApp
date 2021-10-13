//тут будуть типи, які потрібно
export enum Event {
  mouseover = "mouseover",
  mouseout = "mouseout",
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
  walls: { x: number; y: number }[];
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
  first: {
    x: number;
    y: number;
  };
  second: {
    x: number;
    y: number;
  };
  third: {
    x: number;
    y: number;
  };
}
