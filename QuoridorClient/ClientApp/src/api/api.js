export const moveAPI = {
    makeMove() {
        
    }
}
const URL = "https://localhost:5000/";

export const playerAPI = {
    async tryMove(cellFrom , cellTo, turn, opponent, walls) {
        const response = await fetch(URL + "move/" ,
            {method: "GET",
            body: JSON.stringify(cellFrom, cellTo, turn, opponent, walls)})
        return await response.json()
    },
}




