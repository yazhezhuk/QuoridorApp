import CircleIcon from "@mui/icons-material/Circle";
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
