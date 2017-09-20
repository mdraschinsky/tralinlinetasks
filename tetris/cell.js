var Cell = /** @class */ (function () {
    function Cell() {
    }
    Cell.createHtmlElement = function (top, left) {
        var cell = $('<div class="cell"></div>');
        cell.css({
            width: this.size + "px",
            height: this.size + "px",
            top: top + "px",
            left: left + "px"
        });
        return cell;
    };
    Cell.size = 25;
    return Cell;
}());
//# sourceMappingURL=cell.js.map