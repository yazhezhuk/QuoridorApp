//тут будуть типи, які потрібно

export enum Color {
  lightGray = 0xfffafa,
  lavender = 0xE6E6FA,
  white = 0xFFFFFF,
  black = 0x000000

}

export const Theme ={
  wallColor: Color.lightGray,
  cellColor: Color.lavender 
}
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
