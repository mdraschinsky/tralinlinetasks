class PlayfieldCell {
    constructor(public color = '#666666', public value = false) {}
}

type PlayfieldOptions = { id: string, columns: number, visibleRows: number, hiddenRows: number };

class Playfield {
    private columns: number;
    private visibleRows: number;
    private hiddenRows: number;
    private rows: number;

    private field: PlayfieldCell[][];
    private htmlElement: JQuery<HTMLElement>;

    constructor(opt: PlayfieldOptions) {
        this.htmlElement = $(`#${opt.id}`);
        this.columns = opt.columns;
        this.visibleRows = opt.visibleRows;
        this.hiddenRows = opt.hiddenRows;
        this.rows = this.visibleRows + this.hiddenRows;

        this.field = [];
        for (let i = 0; i < this.rows; i++) {
            this.field[i] = [];
            for (let j = 0; j < this.columns; j++) {
                this.field[i][j] = new PlayfieldCell();
            }
        }
    }

    draw() {
        for (let i = this.hiddenRows; i < this.rows; i++) {
            for (let j = 0; j < this.columns; j++) {
                const cell = Cell.createHtmlElement(Cell.size * i, Cell.size * j);
                cell.css('background-color', this.field[i][j].color);
                this.htmlElement.append(cell);
            }
        }
    }

    canBePlaced(top: number, left: number, data: boolean[][]) {
        for(let i = 0; i < data.length; i++) {
            for(let j = 0; j < data[i].length; j++) {
                if(j + left < 0 && data[i][j]) {
                    return false;
                }

                if(!data[i][j]) {
                    continue;
                }

                if(i + top >= this.rows || j + left >= this.columns || this.field[i + top][j + left].value) {
                    return false;
                }
            }
        }

        return true;
    }

    addTetromino(tetromino: Tetromino) {
        for(let i = 0; i < tetromino.model.length; i++) {
            for(let j = 0; j < tetromino.model[i].length; j++) {
                if(tetromino.model[i][j]) {
                    this.field[i + tetromino.top][j + tetromino.left] = new PlayfieldCell(tetromino.color, true);
                }
            }
        }
    }

    submit() {
        let removedRows = 0;
        for(let i = 0; i < this.rows; i++) {
            if(!_.some(this.field[i], c => !c.value)) {
                removedRows++;
                this.field.splice(i, 1);
                const row = _.times(this.columns, () => new PlayfieldCell());
                this.field.unshift(row);
            }
        }

        return removedRows;
    }
}