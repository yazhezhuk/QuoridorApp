import { Box } from "@mui/system";
import {Color, Event, HoveredWall, WallDirection} from "../../Utils";

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
      direction : WallDirection.vertical,
      first: {
      X: position.x,
      Y: position.y,
    },
    second: {
      X: position.x,
      Y: position.y === 16 ? position.y - 1 : position.y + 1,
    },
    third: {
      X: position.x,
      Y: position.y === 16 ? position.y - 2 : position.y + 2,
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
