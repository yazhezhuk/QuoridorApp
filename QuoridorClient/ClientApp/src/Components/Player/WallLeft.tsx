import React, { useState } from "react";
import { Step } from "../../Utils";
import WallItem from "./WallItem";

interface Props {
    playerId:number
    step:Step
}

const WallLeft = ({playerId, step}: Props) => {
    const {remainingWalls} = playerId === 0 ? step.player0 : step.player1
    return(
        <div >
            {new Array(remainingWalls >= 0 ? remainingWalls : 0)
            .fill(1)
            .map((_,x) =>{
                return(
                    <WallItem number={x} key={x}/>
                )
            })}
            {step.stepNumber % 2 === playerId ?
            <div> Your turn </div> : null}
        </div>
    )
}
export default WallLeft;