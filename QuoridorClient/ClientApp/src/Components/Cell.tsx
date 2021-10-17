import { PixiComponent, Stage } from "@inlet/react-pixi";
import { Graphics } from "pixi.js";
import { Color, Event, HoveredWall, Step } from "../Utils";
import Player from "./Player/Player";
import Box from "@mui/material/Box";

interface CellProps {
  x: number;
  y: number;
  step: Step;
  makeMove: (position: { x: number; y: number }) => void;
}

const Cell = ({ x, y, step, makeMove }: CellProps) => {
  const width = (window.screen.availHeight * 0.8) / 11;
  const height = (window.screen.availHeight * 0.8) / 11;
  const position = {
    x: x,
    y: y,
  };
  const { player0, player1 } = step;

  const on0 = player0.x === x && player0.y === y;
  const on1 = player1.x === x && player1.y === y;

  return (
    <Box
      sx={{
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        width: { width },
        height: { height },
        bgcolor: Color.lightGray,
        "&:hover": {
          backgroundColor: Color.lavender,
        },
      }}
      onClick={() => {
        makeMove(position);
      }}
    >
      {on1 && <Player onColor="#0000ff" />}
      {on0 && <Player onColor="#ff0000" />}
    </Box>
  );
};

export default Cell;
