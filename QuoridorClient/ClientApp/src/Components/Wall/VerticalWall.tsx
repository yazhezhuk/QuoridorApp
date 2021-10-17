import { PixiComponent } from "@inlet/react-pixi";
import { Box } from "@mui/system";
import { Graphics } from "pixi.js";
import { Color, Event, HoveredWall } from "../../Utils";

interface WallProps {
  position: { x: number; y: number };
  color: string;
  mouseHover: (hoveredWall: HoveredWall) => void;
  mouseOut: () => void;
  putWall: (hoveredWall: HoveredWall) => void;
}


const VerticalWall = ({ position, color, mouseHover, mouseOut, putWall }: WallProps) => {
  const width = (window.screen.availHeight * 0.8) / 44;
  const height = (window.screen.availHeight * 0.8) / 11;

  const hoveredWall: HoveredWall = {
    first: {
      x: position.x,
      y: position.y,
    },
    second: {
      x: position.x,
      y: position.y === 16 ? position.y - 1 : position.y + 1,
    },
    third: {
      x: position.x,
      y: position.y === 16 ? position.y - 2 : position.y + 2,
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
      onClick ={() => {
        putWall(hoveredWall)
      }}
    />
  );
};

export default VerticalWall;
