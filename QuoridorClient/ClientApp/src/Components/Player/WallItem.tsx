import { Box } from "@material-ui/core";
import React from "react";

interface Props {
    number:number
}

const WallItem = (props: Props) =>{
    return(
        <div>
            <Box
      sx={{
        width: 100,
        height: 25,
        margin:20,
        bgcolor: 'primary.dark',
      }}
    />
        </div>
        
    )
}
export default WallItem