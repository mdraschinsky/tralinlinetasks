var KeyCode;
(function (KeyCode) {
    KeyCode[KeyCode["Up"] = 38] = "Up";
    KeyCode[KeyCode["Right"] = 39] = "Right";
    KeyCode[KeyCode["Left"] = 37] = "Left";
    KeyCode[KeyCode["Down"] = 40] = "Down";
})(KeyCode || (KeyCode = {}));
var Game = /** @class */ (function () {
    function Game() {
        this.score = 0;
        this.level = 1;
        this.scoresByRow = 100;
        this.scoresForNextLevel = 1000;
        this.diffLevelSpeed = 100;
        this.tetrominoFactory = new TetrominoFactory();
        this.playfieldOptions = {
            id: 'playfield',
            columns: 10,
            visibleRows: 20,
            hiddenRows: 2
        };
        this.playfield = new Playfield(this.playfieldOptions);
        $('#stats').css('top', (this.playfieldOptions.hiddenRows * Cell.size) + "px");
        $('#stats').css('left', (this.playfieldOptions.columns * Cell.size) + 20 + "px");
        $('#total').css('top', ((this.playfieldOptions.visibleRows - 2) * Cell.size) + "px");
        this.item = $('#tetromino');
        this.nextTetrominoContainer = $('#nextTetromino');
    }
    Game.prototype.start = function () {
        var _this = this;
        this.playfield.draw();
        this.drawTotal();
        this.currentTetromino = this.tetrominoFactory.getRandomTetromino();
        this.currentTetromino.setStartPosition(this.playfieldOptions.columns);
        this.nextTetromino = this.tetrominoFactory.getRandomTetromino();
        this.drawTetromino(this.item, this.currentTetromino);
        this.drawTetromino(this.nextTetrominoContainer, this.nextTetromino, true);
        window.onkeyup = function (e) {
            var top = _this.currentTetromino.top;
            var left = _this.currentTetromino.left;
            var model = _this.currentTetromino.model;
            if (top + _this.currentTetromino.lastVisibleRow < _this.playfieldOptions.hiddenRows) {
                return;
            }
            switch (e.keyCode) {
                case KeyCode.Right:
                    left++;
                    break;
                case KeyCode.Left:
                    left--;
                    break;
                case KeyCode.Up:
                    model = _this.currentTetromino.getRotatedModel();
                    break;
                case KeyCode.Down:
                    top++;
                    break;
                default:
                    return;
            }
            if (_this.playfield.canBePlaced(top, left, model)) {
                _this.currentTetromino.top = top;
                _this.currentTetromino.left = left;
                _this.currentTetromino.model = model;
                _this.drawTetromino(_this.item, _this.currentTetromino);
            }
        };
        this.play(1000);
    };
    Game.prototype.play = function (milliseconds) {
        var _this = this;
        var timerId = setInterval(function () {
            var top = _this.currentTetromino.top + 1;
            if (_this.playfield.canBePlaced(top, _this.currentTetromino.left, _this.currentTetromino.model)) {
                _this.currentTetromino.top = top;
                _this.drawTetromino(_this.item, _this.currentTetromino);
            }
            else {
                if (_this.playfield.canBePlaced(_this.currentTetromino.top, _this.currentTetromino.left, _this.currentTetromino.model)) {
                    _this.playfield.addTetromino(_this.currentTetromino);
                    var score = _this.playfield.submit() * _this.scoresByRow;
                    _this.playfield.draw();
                    _this.currentTetromino = _this.nextTetromino;
                    _this.currentTetromino.setStartPosition(_this.playfieldOptions.columns);
                    _this.nextTetromino = _this.tetrominoFactory.getRandomTetromino();
                    _this.drawTetromino(_this.item, _this.currentTetromino);
                    _this.drawTetromino(_this.nextTetrominoContainer, _this.nextTetromino, true);
                    if (score > 0) {
                        _this.score += score;
                        var level = Math.floor(_this.score / _this.scoresForNextLevel) + 1;
                        if (level > _this.level) {
                            _this.level++;
                            clearInterval(timerId);
                            _this.play(milliseconds - _this.diffLevelSpeed);
                            _this.drawTotal();
                            return;
                        }
                        _this.drawTotal();
                    }
                }
                else {
                    $('#gameover').show();
                    clearInterval(timerId);
                }
            }
        }, milliseconds);
    };
    Game.prototype.drawTotal = function () {
        $('#total').html("Score: " + this.score + "<p/>Level: " + this.level);
    };
    Game.prototype.drawTetromino = function (container, tetromino, isNextTetromino) {
        if (isNextTetromino === void 0) { isNextTetromino = false; }
        container.empty();
        for (var i = 0; i < tetromino.model.length; i++) {
            for (var j = 0; j < tetromino.model[i].length; j++) {
                if (tetromino.model[i][j] && (isNextTetromino || tetromino.top + i >= this.playfieldOptions.hiddenRows)) {
                    var cell = Cell.createHtmlElement(Cell.size * i, Cell.size * j);
                    cell.css('background-color', tetromino.color);
                    container.append(cell);
                }
            }
        }
        container.css('top', (tetromino.top * Cell.size) + "px");
        container.css('left', (tetromino.left * Cell.size) + "px");
    };
    return Game;
}());
//# sourceMappingURL=game.js.map