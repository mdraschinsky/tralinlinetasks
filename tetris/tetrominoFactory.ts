enum TetrominoType {
    Straight,
    LeftGun,
    RightGun,
    Square,
    RightSnake,
    Triangle,
    LeftSnake
}

class Tetromino {
    top: number = 0;
    left: number = 0;
    lastVisibleRow: number;

    constructor(public color: string, public model: boolean[][]) {
        for(let i = this.model.length - 1; i >= 0; i--) {
            if(!_.some(this.model[i], cell => !cell)) {
                this.lastVisibleRow = i;
                break;
            }
        }
    }

    getRotatedModel() {
        const result = [];
        for (let i = 0; i < this.model[0].length; i++) {
            result[i] = [];
            for (let j = 0; j < this.model.length; j++) {
                result[i][this.model.length - j - 1] = this.model[j][i];
            }
        }
    
        return result;
    }

    setStartPosition(playFieldColumns: number) {
        this.left = (Math.ceil(playFieldColumns / 2) - 1 ) - (Math.ceil(this.model[0].length / 2) - 1);
    }
}

class TetrominoLimit {
    private counter: number = 0;

    constructor(public type: TetrominoType, public limit: number) {}

    get reserve() {
        return this.limit - this.counter;
    }

    updateCounter(type: TetrominoType) {
        if(this.type != type) {
            this.counter++;
        } else {
            this.counter = 0;
        }
    }
}

class TetrominoFactory {
    bagSize: number =  7;
    bag: TetrominoType[] = [];
    tetrominoLimits: TetrominoLimit[] = [
        new TetrominoLimit(TetrominoType.Straight, 12),
        new TetrominoLimit(TetrominoType.RightSnake, 4),
        new TetrominoLimit(TetrominoType.LeftSnake, 4)
    ];

    tetrominoTypes = [TetrominoType.Straight, TetrominoType.LeftGun, TetrominoType.RightGun,
        TetrominoType.Square, TetrominoType.RightSnake, TetrominoType.Triangle, TetrominoType.LeftSnake];

    create(type: TetrominoType): Tetromino {
        switch(type) {

            case TetrominoType.Straight:
                return new Tetromino("#00FFFF",[
                        [false, false, false, false],
                        [true, true, true, true],
                        [false, false, false, false],
                        [false, false, false, false]
                    ]);

            case TetrominoType.LeftGun:
                return new Tetromino("#0000FF", [
                        [true, false, false],
                        [true, true, true],
                        [false, false, false]
                    ]);

            case TetrominoType.RightGun:
                return new Tetromino("#FFAA00", [
                        [false, false, true],
                        [true, true, true],
                        [false, false, false]
                    ]);

            case TetrominoType.Square:
                return new Tetromino("#FFFF00", [
                        [false, false, false, false],
                        [false, true, true, false],
                        [false, true, true, false],
                        [false, false, false, false]
                    ]);

            case TetrominoType.RightSnake:
                return new Tetromino("#00FF00", [
                        [false, true, true],
                        [true, true, false],
                        [false, false, false]
                    ]);

            case TetrominoType.Triangle:
                return new Tetromino("#9900FF", [
                        [false, true, false],
                        [true, true, true],
                        [false, false, false]
                    ]);

            case TetrominoType.LeftSnake:
                return new Tetromino("#FF0000", [
                        [true, true, false],
                        [false, true, true],
                        [false, false, false]
                    ]);
        }
    }

    getRandomTetromino() {
        if(this.bag.length == 0) {
            this.generateBag();
        }

        const tetrominoType = this.bag.shift();
        return this.create(tetrominoType);
    }

    private generateBag() {
        this.bag = [];
        for(let i = 0; i < this.bagSize; i++) {
            let minTetrominoLimits = _.minBy(this.tetrominoLimits, t => t.reserve);
            const type = minTetrominoLimits.reserve < 1 ?  minTetrominoLimits.type : _.sample(this.tetrominoTypes);
            this.tetrominoLimits.forEach(tl => tl.updateCounter(type));
            this.bag.push(type);
        }
    }
}