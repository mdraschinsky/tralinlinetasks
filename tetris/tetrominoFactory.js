var TetrominoType;
(function (TetrominoType) {
    TetrominoType[TetrominoType["Straight"] = 0] = "Straight";
    TetrominoType[TetrominoType["LeftGun"] = 1] = "LeftGun";
    TetrominoType[TetrominoType["RightGun"] = 2] = "RightGun";
    TetrominoType[TetrominoType["Square"] = 3] = "Square";
    TetrominoType[TetrominoType["RightSnake"] = 4] = "RightSnake";
    TetrominoType[TetrominoType["Triangle"] = 5] = "Triangle";
    TetrominoType[TetrominoType["LeftSnake"] = 6] = "LeftSnake";
})(TetrominoType || (TetrominoType = {}));
var Tetromino = /** @class */ (function () {
    function Tetromino(color, model) {
        this.color = color;
        this.model = model;
        this.top = 0;
        this.left = 0;
        for (var i = this.model.length - 1; i >= 0; i--) {
            if (!_.some(this.model[i], function (cell) { return !cell; })) {
                this.lastVisibleRow = i;
                break;
            }
        }
    }
    Tetromino.prototype.getRotatedModel = function () {
        var result = [];
        for (var i = 0; i < this.model[0].length; i++) {
            result[i] = [];
            for (var j = 0; j < this.model.length; j++) {
                result[i][this.model.length - j - 1] = this.model[j][i];
            }
        }
        return result;
    };
    Tetromino.prototype.setStartPosition = function (playFieldColumns) {
        this.left = (Math.ceil(playFieldColumns / 2) - 1) - (Math.ceil(this.model[0].length / 2) - 1);
    };
    return Tetromino;
}());
var TetrominoLimit = /** @class */ (function () {
    function TetrominoLimit(type, limit) {
        this.type = type;
        this.limit = limit;
        this.counter = 0;
    }
    Object.defineProperty(TetrominoLimit.prototype, "reserve", {
        get: function () {
            return this.limit - this.counter;
        },
        enumerable: true,
        configurable: true
    });
    TetrominoLimit.prototype.updateCounter = function (type) {
        if (this.type != type) {
            this.counter++;
        }
        else {
            this.counter = 0;
        }
    };
    return TetrominoLimit;
}());
var TetrominoFactory = /** @class */ (function () {
    function TetrominoFactory() {
        this.bagSize = 7;
        this.bag = [];
        this.tetrominoLimits = [
            new TetrominoLimit(TetrominoType.Straight, 12),
            new TetrominoLimit(TetrominoType.RightSnake, 4),
            new TetrominoLimit(TetrominoType.LeftSnake, 4)
        ];
        this.tetrominoTypes = [TetrominoType.Straight, TetrominoType.LeftGun, TetrominoType.RightGun,
            TetrominoType.Square, TetrominoType.RightSnake, TetrominoType.Triangle, TetrominoType.LeftSnake];
    }
    TetrominoFactory.prototype.create = function (type) {
        switch (type) {
            case TetrominoType.Straight:
                return new Tetromino("#00FFFF", [
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
    };
    TetrominoFactory.prototype.getRandomTetromino = function () {
        if (this.bag.length == 0) {
            this.generateBag();
        }
        var tetrominoType = this.bag.shift();
        return this.create(tetrominoType);
    };
    TetrominoFactory.prototype.generateBag = function () {
        this.bag = [];
        var _loop_1 = function (i) {
            var minTetrominoLimits = _.minBy(this_1.tetrominoLimits, function (t) { return t.reserve; });
            var type = minTetrominoLimits.reserve < 1 ? minTetrominoLimits.type : _.sample(this_1.tetrominoTypes);
            this_1.tetrominoLimits.forEach(function (tl) { return tl.updateCounter(type); });
            this_1.bag.push(type);
        };
        var this_1 = this;
        for (var i = 0; i < this.bagSize; i++) {
            _loop_1(i);
        }
    };
    return TetrominoFactory;
}());
//# sourceMappingURL=tetrominoFactory.js.map