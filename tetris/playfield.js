var PlayfieldCell = /** @class */ (function () {
    function PlayfieldCell(color, value) {
        if (color === void 0) { color = '#666666'; }
        if (value === void 0) { value = false; }
        this.color = color;
        this.value = value;
    }
    return PlayfieldCell;
}());
var Playfield = /** @class */ (function () {
    function Playfield(opt) {
        this.htmlElement = $("#" + opt.id);
        this.columns = opt.columns;
        this.visibleRows = opt.visibleRows;
        this.hiddenRows = opt.hiddenRows;
        this.rows = this.visibleRows + this.hiddenRows;
        this.field = [];
        for (var i = 0; i < this.rows; i++) {
            this.field[i] = [];
            for (var j = 0; j < this.columns; j++) {
                this.field[i][j] = new PlayfieldCell();
            }
        }
    }
    Playfield.prototype.draw = function () {
        for (var i = this.hiddenRows; i < this.rows; i++) {
            for (var j = 0; j < this.columns; j++) {
                var cell = Cell.createHtmlElement(Cell.size * i, Cell.size * j);
                cell.css('background-color', this.field[i][j].color);
                this.htmlElement.append(cell);
            }
        }
    };
    Playfield.prototype.canBePlaced = function (top, left, data) {
        for (var i = 0; i < data.length; i++) {
            for (var j = 0; j < data[i].length; j++) {
                if (j + left < 0 && data[i][j]) {
                    return false;
                }
                if (!data[i][j]) {
                    continue;
                }
                if (i + top >= this.rows || j + left >= this.columns || this.field[i + top][j + left].value) {
                    return false;
                }
            }
        }
        return true;
    };
    Playfield.prototype.addTetromino = function (tetromino) {
        for (var i = 0; i < tetromino.model.length; i++) {
            for (var j = 0; j < tetromino.model[i].length; j++) {
                if (tetromino.model[i][j]) {
                    this.field[i + tetromino.top][j + tetromino.left] = new PlayfieldCell(tetromino.color, true);
                }
            }
        }
    };
    Playfield.prototype.submit = function () {
        var removedRows = 0;
        for (var i = 0; i < this.rows; i++) {
            if (!_.some(this.field[i], function (c) { return !c.value; })) {
                removedRows++;
                this.field.splice(i, 1);
                var row = _.times(this.columns, function () { return new PlayfieldCell(); });
                this.field.unshift(row);
            }
        }
        return removedRows;
    };
    return Playfield;
}());
//# sourceMappingURL=playfield.js.map