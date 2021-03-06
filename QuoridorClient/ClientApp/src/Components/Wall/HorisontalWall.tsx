import {Box} from "@mui/system";
import {HoveredWall, WallDirection} from "../../Utils";

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
    direction : WallDirection.horizontal,
    first: {
      X: position.x,
      Y: position.y,
    },
    second: {
      X: position.x === 16 ? position.x - 1 : position.x + 1,
      Y: position.y,
    },
    third: {
      X: position.x === 16 ? position.x - 2 : position.x + 2,
      Y: position.y,
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
