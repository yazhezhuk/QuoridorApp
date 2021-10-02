import { PixiComponent } from '@inlet/react-pixi'
import { Graphics } from 'pixi.js'

interface WallProps {
  x: number
  y: number
  width: number
  height: number
  color: number
}

const VerticalWall = PixiComponent<WallProps, Graphics>('VerticalWall', {
  create: () => new Graphics(),
  applyProps: (ins, _, props) => {
    
    const width =  window.screen.availHeight * 0.8 / 44;
    const height =window.screen.availHeight * 0.8 / 11;
    const summ = width + height
    const position = {
      x:(summ/2)*props.x + height,
      y:props.y*summ/2
    }
    
    ins.beginFill(props.color)
    ins.drawRect(position.x, position.y, width, height)
    ins.endFill()
  },
})
export default VerticalWall;