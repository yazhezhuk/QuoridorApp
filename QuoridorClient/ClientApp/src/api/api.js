import {WallToSend} from "../Utils";

export const moveAPI = {
    makeMove() {

    }
}
const URL = "http://localhost:9000/";

export interface Position {
    X : number,
    Y : number
}
export interface WallPosition {
    X : number,
    Y : number,
    Direction : string
}

export const playerAPI = {
    async tryMove(cellFrom: Position,
                  cellTo: Position,
                  turn: number,
                  opponent: Position,
                  walls: WallToSend[]) {
        const response = await fetch(URL + "move",
            {
                method: "POST",
                headers: {
                    'Content-Type': 'text/json',
                },
                body: JSON.stringify({
                    cellFrom,
                    cellTo,
                    turn,
                    opponent,
                    walls
                }),
            }
        )
        return (await response.json())["CanExecute"];
    },
    async tryWall(current: Position,
                  opponent: Position,
                  turn: number,
                  toSetup: WallToSend,
                  otherWalls: WallToSend[]) {
        const response = await fetch(URL + "move/wall/",
            {
                method: "POST",
                headers: {
                    'Content-Type': 'text/json',
                },
                body: JSON.stringify({
                    current,
                    opponent,
                    turn,
                    toSetup,
                    otherWalls
                }),
            }
        )
        return (await response.json())["CanExecute"];
    },
}