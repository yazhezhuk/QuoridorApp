import React, { useEffect, useState } from "react";
import "./App.css";
import Field from "./Components/Field";
import WallLeft from "./Components/Player/WallLeft";
import { Color, HoveredWall, isEven, Step, WallDirection} from "./Utils";
import {moveAPI, playerAPI} from "./api/api";

const initialStep = {
  player0: {
    x: 0,
    y: 8,
    remainingWalls: 10,
  },
  player1: {
    x: 16,
    y: 8,
    remainingWalls: 10,
  },
  walls: [],
  wallsToSend: [],
  stepNumber: 0,
};

function App() {
  const [step, setStep] = useState<Step>(initialStep);

  const [isHover, setHover] = useState<boolean[][]>(
      new Array(17).fill(false).map((_) => {
        return new Array(17).fill(false);
      })
  );
  const mouseHover = (hoveredWall: HoveredWall) => {
    const temp = new Array(17).fill(false).map((_) => {
      return new Array(17).fill(false);
    });

    const {first, second, third} = hoveredWall;

    temp[first.Y][first.X] = true;
    temp[second.Y][second.X] = true;
    temp[third.Y][third.X] = true;
    // else: wallIntersect
    setHover(temp);
  };

  const mouseOut = () => {
    const temp = new Array(17).fill(false).map((_) => {
      return new Array(17).fill(false);
    });
    setHover(temp);
  };

  const makeMove = async (position: { x: number, y: number }) => {
    const { player0, player1, stepNumber } = step;
    const [...walls] = step.walls;
    const [...wallsToSend] = step.wallsToSend;

    const nextStep = {
      walls,
      wallsToSend,
      player0: {
        x: player0.x,
        y: player0.y,
        remainingWalls: step.player0.remainingWalls,
      },
      player1: {
        x: player1.x,
        y: player1.y,
        remainingWalls: step.player1.remainingWalls,
      },
      stepNumber: step.stepNumber + 1,
    };
    debugger
    if (isEven(stepNumber)) {
      playerAPI.tryMove({X: player0.y / 2, Y: player0.x / 2 },
          {Y: position.x / 2, X: position.y / 2},
          step.stepNumber,
          {Y: player1.x / 2, X: player1.y / 2},
          wallsToSend)
          .then(result => {
            if (result) {
              nextStep.player0.x = position.x;
              nextStep.player0.y = position.y;
              setStep(nextStep)
            }
          })
    }
    else {
      playerAPI.tryMove({Y: player1.x / 2, X: player1.y / 2},
          {Y: position.x / 2, X: position.y / 2},
          step.stepNumber,
          {Y: player0.x / 2, X: player0.y / 2},
          wallsToSend)
          .then(result => {
            if (result) {
              nextStep.player1.x = position.x;
              nextStep.player1.y = position.y;
              setStep(nextStep)
            }
          })
      }
    }

    const putWall = (position: HoveredWall) => {
      const { stepNumber } = step;
      const [...walls] = step.walls;
      const [...wallsToSend] = step.wallsToSend;
      const { first, second, third,direction } = position;

      const nextStep = {
        walls,
        wallsToSend,
        player0: {
          x: step.player0.x,
          y: step.player0.y,
          remainingWalls: step.player0.remainingWalls,
        },
        player1: {
          x: step.player1.x,
          y: step.player1.y,
          remainingWalls: step.player1.remainingWalls,
        },
        stepNumber: step.stepNumber + 1,
      };
      const desiredPosition = [first, second, third];

      const tempWalls = [...wallsToSend,{direction,position:{X : first.X / 2,Y: first.Y / 2}}]

      if (isEven(stepNumber)) {
        playerAPI.tryWall({Y : step.player0.x / 2, X : step.player0.y / 2},
            {Y: step.player1.x / 2, X: step.player1.y / 2},
            step.stepNumber,
            {direction,position:{X : Math.trunc(first.X / 2),Y: Math.trunc(first.Y / 2)}},
            wallsToSend)
            .then(result => {
              if (step.player0.remainingWalls === 0 || !result) {
                return;
              }
              nextStep.player0.remainingWalls -= 1;
              nextStep.walls = [...walls, ...desiredPosition];
              nextStep.wallsToSend = [...wallsToSend, {direction : direction,position:{X : Math.trunc(first.X / 2),Y: Math.trunc(first.Y / 2)}}]
              setStep(nextStep);
            })
      } else {
        playerAPI.tryWall({Y : step.player1.x / 2, X : step.player1.y / 2},
            {Y: step.player0.x / 2, X: step.player0.y / 2},
            step.stepNumber,
            {direction,position:{X : Math.trunc(first.X / 2),Y: Math.trunc(first.Y / 2)}},
            wallsToSend)
            .then(result => {
              if (step.player1.remainingWalls === 0 || !result) {
                return;
              }
              nextStep.player0.remainingWalls -= 1;
              nextStep.walls = [...walls, ...desiredPosition];
              nextStep.wallsToSend = [...wallsToSend, {direction : direction,position:{X : Math.trunc(first.X / 2),Y: Math.trunc(first.Y / 2)}}]
              setStep(nextStep);
            })
      }
    };


    return (
        <div className="App">
          <WallLeft playerId={0} step={step}/>
          <Field
              step={step}
              wallColor={Color.lavender}
              cellColor={Color.lightGray}
              isHover={isHover}
              mouseHover={mouseHover}
              mouseOut={mouseOut}
              putWall={putWall}
              makeMove={makeMove}
          />
          <WallLeft playerId={1} step={step}/>
        </div>
    );
}
  export default App;
