import { Graphics } from "pixi.js";
import { PixiComponent } from "@inlet/react-pixi";
import CircleIcon from "@mui/icons-material/Circle";
import { Icon } from "@mui/material";
interface PlayerProps {
  onColor: string;
}

const Player = ({ onColor }: PlayerProps) => {
  const width = (window.screen.availHeight * 0.8) / 11;
  return (
    <CircleIcon color="primary" sx={{ fontSize: width, color: onColor }} />
  );
};

export default Player;
