import { PixiComponent } from "@inlet/react-pixi";
import { Box } from "@mui/system";
import { Graphics } from "pixi.js";
import { Color, HoveredWall } from "../../Utils";

interface WallProps {
  position: { x: number; y: number };
  color: string;
  mouseHover: (hoveredWall: HoveredWall) => void;
  mouseOut: () => void;
  putWall: (hoveredWall: HoveredWall) => void;
}

const HorisontalWall = ({
  position,
  color,
  mouseHover,
  mouseOut,
  putWall,
}: WallProps) => {
  const height = (window.screen.availHeight * 0.8) / 44;
  const width = (window.screen.availHeight * 0.8) / 11;

  const hoveredWall: HoveredWall = {
    first: {
      x: position.x,
      y: position.y,
    },
    second: {
      x: position.x === 16 ? position.x - 1 : position.x + 1,
      y: position.y,
    },
    third: {
      x: position.x === 16 ? position.x - 2 : position.x + 2,
      y: position.y,
    },
  };
  return (
    <Box
      sx={{
        width: { width },
        height: { height },
        bgcolor: color,
      }}
      onMouseOver={() => {
        console.log(color);
        mouseHover(hoveredWall);
      }}
      onMouseLeave={() => {
        mouseOut();
      }}
      onClick={() => {
        putWall(hoveredWall);
      }}
    />
  );
};

export default HorisontalWall;
