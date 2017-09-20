enum KeyCode {
    Up = 38,
    Right = 39,
    Left = 37,
    Down = 40
}

class Game {
    playfield: Playfield;
    item: JQuery<HTMLElement>;
    nextTetrominoContainer: JQuery<HTMLElement>;
    score: number = 0;
    level: number = 1;
    scoresByRow: number = 100;
    scoresForNextLevel: number = 1000;
    diffLevelSpeed: number = 100;

    currentTetromino: Tetromino;
    nextTetromino: Tetromino;

    tetrominoFactory = new TetrominoFactory();

    playfieldOptions: PlayfieldOptions = {
        id: 'playfield',
        columns: 10,
        visibleRows: 20,
        hiddenRows: 2
    };

    constructor() {
        this.playfield = new Playfield(this.playfieldOptions);
        $('#stats').css('top', `${(this.playfieldOptions.hiddenRows * Cell.size)}px`);
        $('#stats').css('left', `${(this.playfieldOptions.columns * Cell.size) + 20 }px`);
        $('#total').css('top', `${((this.playfieldOptions.visibleRows - 2) * Cell.size)}px`);
        this.item = $('#tetromino');
        this.nextTetrominoContainer = $('#nextTetromino');
    }

    start() {
        this.playfield.draw();
        this.drawTotal();

        this.currentTetromino = this.tetrominoFactory.getRandomTetromino();
        this.currentTetromino.setStartPosition(this.playfieldOptions.columns);
        this.nextTetromino = this.tetrominoFactory.getRandomTetromino();

        this.drawTetromino(this.item, this.currentTetromino);
        this.drawTetromino(this.nextTetrominoContainer, this.nextTetromino, true);
    
        window.onkeyup = (e: KeyboardEvent) => {
            let top = this.currentTetromino.top;
            let left = this.currentTetromino.left;
            let model = this.currentTetromino.model;

            if(top + this.currentTetromino.lastVisibleRow < this.playfieldOptions.hiddenRows) {
                return;
            }
    
            switch(e.keyCode) {
                case KeyCode.Right:
                    left++;
                    break;
                case KeyCode.Left:
                    left--;
                    break;
                case KeyCode.Up:
                    model = this.currentTetromino.getRotatedModel();
                    break;
                case KeyCode.Down:
                    top++;
                    break;
                default:
                    return;
            }
    
            if(this.playfield.canBePlaced(top, left, model)) {
                this.currentTetromino.top = top;
                this.currentTetromino.left = left;
                this.currentTetromino.model = model;
                this.drawTetromino(this.item, this.currentTetromino);
            }
        }

        this.play(1000);
    }

    play(milliseconds: number) {
        const timerId = setInterval(() => {
            const top = this.currentTetromino.top + 1;
            if(this.playfield.canBePlaced(top, this.currentTetromino.left, this.currentTetromino.model)) {
                this.currentTetromino.top = top;
                this.drawTetromino(this.item, this.currentTetromino);
            }
            else {
                if(this.playfield.canBePlaced(this.currentTetromino.top, this.currentTetromino.left, this.currentTetromino.model)) {
                    this.playfield.addTetromino(this.currentTetromino);
                    
                    const score = this.playfield.submit() * this.scoresByRow;
                    
                    this.playfield.draw();
                    this.currentTetromino = this.nextTetromino;
                    this.currentTetromino.setStartPosition(this.playfieldOptions.columns);
                    this.nextTetromino = this.tetrominoFactory.getRandomTetromino();
                    this.drawTetromino(this.item, this.currentTetromino);
                    this.drawTetromino(this.nextTetrominoContainer, this.nextTetromino, true);

                    if(score > 0) {
                        this.score += score;
                        const level = Math.floor(this.score / this.scoresForNextLevel) + 1;
                        if(level > this.level) {
                            this.level++;
                            clearInterval(timerId);
                            this.play(milliseconds - this.diffLevelSpeed);
                            this.drawTotal();
                            return;
                        }
                        this.drawTotal();
                    }
                }
                else {
                    $('#gameover').show();
                    clearInterval(timerId);
                }
            }
        }, milliseconds);
    }

    drawTotal() {
        $('#total').html(`Score: ${this.score}<p/>Level: ${this.level}`);
    }

    drawTetromino(container: JQuery<HTMLElement>, tetromino: Tetromino, isNextTetromino: boolean = false) {
        container.empty();
        for (let i = 0; i < tetromino.model.length; i++) {
            for (let j = 0; j < tetromino.model[i].length; j++) {
                if (tetromino.model[i][j] && (isNextTetromino || tetromino.top + i >= this.playfieldOptions.hiddenRows)) {
                    const cell = Cell.createHtmlElement(Cell.size * i, Cell.size * j);
                    cell.css('background-color', tetromino.color);
                    container.append(cell);
                }
            }
        }
    
        container.css('top', `${(tetromino.top * Cell.size)}px`);
        container.css('left', `${(tetromino.left * Cell.size)}px`);
    }
}