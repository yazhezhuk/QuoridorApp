import { PixiComponent } from '@inlet/react-pixi'
import { Graphics } from 'pixi.js'
import { Color, Step } from '../Utils'
import Player from "./Player/Player"
interface CellProps {
  x: number
  y: number
  color: Color
  step: Step
}

const Cell = PixiComponent<CellProps, Graphics>('Cell', {
  create: () => new Graphics(),
  applyProps: (ins, _, props) => {
    const {player0, player1} = props.step
    const size = window.screen.availHeight * 0.8 / 11
    const summ = window.screen.availHeight * 0.8 / 11 + window.screen.availHeight * 0.8 / 44
    const position ={
      x: summ / 2 * props.x,
      y: summ / 2 * props.y
    }

    const on0 = player0.x === props.x && player0.y === props.y;
    const on1 = player1.x === props.x && player1.y === props.y;
    const onState = on0 || on1;
 
    ins.beginFill(props.color)
    console.log(size)
    ins.drawRect(position.x , position.y, size, size)
    ins.endFill()


  {onState && 
    ins.beginFill(0xff0000)
    ins.drawCircle(position.x+size/2, position.y+size/2, size/2*0.9)
    ins.endFill()
    }
    
    
  }
})
export default Cell;