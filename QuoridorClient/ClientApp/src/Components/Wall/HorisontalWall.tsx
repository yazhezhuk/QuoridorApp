import { PixiComponent } from '@inlet/react-pixi'
import { Graphics } from 'pixi.js'

interface WallProps {
  x: number
  y: number
  width: number
  height: number
  color: number
}

const HorisontalWall = PixiComponent<WallProps, Graphics>('HorisontalWall', {
  create: () => new Graphics(),
  applyProps: (ins, _, props) => {
    const height =  window.screen.availHeight * 0.8 / 44;
    const width =window.screen.availHeight * 0.8 / 11;
    const summ = width+height
    const position = {
      x: summ / 2 * props.x,
      y:(summ/2)*props.y + width
    }
   
    ins.beginFill(props.color)
    ins.drawRect(position.x, position.y, width, height)
    ins.endFill()
  },
})
export default HorisontalWall;