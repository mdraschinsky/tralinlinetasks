class Cell {
    static size = 25;

    static createHtmlElement(top: number, left: number) {
        const cell = $('<div class="cell"></div>');
        cell.css({
            width: `${this.size}px`,
            height: `${this.size}px`,
            top: `${top}px`,
            left: `${left}px`
        });
        return cell;
    }
}