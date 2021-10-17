import { Modal, Box, Typography, Button } from "@mui/material";
import React from "react";

interface WinnerProps {
  open: boolean;
  setOpen: () => void
  winner:number
}
const style = {
  position: 'absolute' as 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 400,
  bgcolor: 'background.paper',
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
};

const Winner = ({ open,setOpen,winner }: WinnerProps) => {
  return (
    <Modal
      open={open}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Typography id="modal-modal-title" variant="h6" component="h2">
          Player {winner} won!!!
        </Typography>
        <Typography id="modal-modal-description" sx={{ mt: 2 }}>
            Press the button to start new game.
          </Typography>
        <Button onClick={()=>{setOpen()}}>Start New</Button>
      </Box>
    </Modal>
  );
};
export default Winner;
