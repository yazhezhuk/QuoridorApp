import React, { useEffect, useState } from "react";
import "./App.css";
import Field from "./Components/Field";
import WallLeft from "./Components/Player/WallLeft";
import { Color, HoveredWall, isEven, Step } from "./Utils";

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

    const { first, second, third } = hoveredWall;

    temp[first.y][first.x] = true;
    temp[second.y][second.x] = true;
    temp[third.y][third.x] = true;
    // else: wallIntersect
    setHover(temp);
  };

  const mouseOut = () => {
    const temp = new Array(17).fill(false).map((_) => {
      return new Array(17).fill(false);
    });
    setHover(temp);
  };

  const makeMove = (position:{x:number, y:number}) => {
    const { player0, player1, stepNumber } = step;
    const [...walls] = step.walls;
    
    const nextStep = {
      walls,
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
    if (isEven(stepNumber)){
      nextStep.player0.x = position.x;
      nextStep.player0.y = position.y;
    }else{
      nextStep.player1.x = position.x;
      nextStep.player1.y = position.y;
    }
    setStep(nextStep)
  };

  const putWall = (position: HoveredWall) => {
    const { player0, player1, stepNumber } = step;
    const [...walls] = step.walls;
    const { first, second, third } = position;

    const nextStep = {
      walls,
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

    if (isEven(stepNumber)) {
      if (step.player0.remainingWalls === 0) return;
      nextStep.player0.remainingWalls -= 1;
    } else {
      if (step.player1.remainingWalls === 0) return;
      nextStep.player1.remainingWalls -= 1;
    }
    nextStep.walls = [...walls, ...desiredPosition];
    setStep(nextStep);
  };
  

  return (
    <div className="App">
      <WallLeft playerId={0} step={step} />
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
      <WallLeft playerId={1} step={step} />
    </div>
  );
}

export default App;
