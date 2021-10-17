import { Box } from '@mui/system';

interface WallProps {
  position: { x: number; y: number };
  color: string;

}
const IntersectionWall = ({ position, color }:WallProps) => {
  const height = (window.screen.availHeight * 0.8) / 44;
  const width =(window.screen.availHeight * 0.8) / 44;
  return (
    <Box
      sx={{
        width: { width },
        height: { height },
        bgcolor: color
      }}
     
    />
  );
};

export default IntersectionWall;