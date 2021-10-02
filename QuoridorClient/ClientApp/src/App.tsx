import React, { useState } from 'react';
import './App.css';
import Field from './Components/Field';
import WallLeft from './Components/Player/WallLeft';
import { Color, Step } from './Utils';



const initialStep ={
  player0:{
    x: 0,
    y: 8,
    remainingWalls: 10
  },
  player1:{
    x: 16,
    y: 8,
    remainingWalls: 10
  },
  walls: [],
  stepNumber: 0,
}

function App() {
  
  const [step, setStep] = useState<Step>(initialStep)



  return (
    <div className="App">
      <WallLeft playerId={0} step={step} />
      <Field step={step}
             wallColor={Color.lavender} 
             cellColor={Color.lightGray} 

             />
      <WallLeft playerId={1} step={step} />
    </div>
     )
}

export default App;
