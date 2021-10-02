import {Container, Stage} from "@inlet/react-pixi";
import React from "react";
import {Color, Step} from "../Utils";

import Cell from "./Cell";
import HorisontalWall from "./Wall/HorisontalWall";
import IntersectionWall from "./Wall/IntersectionWall";
import VerticalWall from "./Wall/VerticalWall";

interface Props {
    step: Step
    wallColor: Color
    cellColor: Color

}

const Field = ({step, wallColor, cellColor}: Props) => {
    const width = window.screen.availHeight * 0.8
    const height = window.screen.availHeight * 0.8
    const stageProps = {
        height,
        width,
        options: {
            backgroundAlpha: 1,

        },
    }
    console.log(window.screen)
    return (
        <Stage {...stageProps} >
            {new Array(17).fill(1).map((_, y) => {
                return (
                    <Container key={y}>
                        {new Array(17).fill(1).map((_, x) => {

                            if (y % 2 === 0 && x % 2 === 0) {

                                return (
                                    <Cell x={x} y={y} color={cellColor} step={step} key={x ** 2 + y}/>
                                )

                            } else if (x % 2 === 1 && y % 2 === 0) {
                                return (
                                    <VerticalWall x={(x - 1)} y={y} width={5} height={20} color={wallColor}
                                                  key={y ** 2 + x}/>
                                )
                            } else if (y % 2 === 1 && x % 2 === 0) {
                                return (
                                    <HorisontalWall x={x} y={y - 1} width={20} height={5} color={wallColor}
                                                    key={y ** 2 + x}/>
                                )
                            } else {
                                return (
                                    <IntersectionWall x={x - 1} y={y - 1} width={5} height={5} color={wallColor}
                                                      key={y ** 2 + x}/>
                                )
                            }

                        })}

                    </Container>


                )
            })
            }
        </Stage>
    )
}

export default Field