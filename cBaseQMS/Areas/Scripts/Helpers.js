$.prototype.enable = function () {
    $.each(this, function (index, el) {
        $(el).removeAttr('disabled');
    });
}

$.prototype.disable = function () {
    $.each(this, function (index, el) {
        $(el).prop('disabled', 'disabled');
    });
}

$.fn.disabled = function (isDisabled) {
    if (isDisabled) {
        this.attr('disabled', 'disabled');
    } else {
        this.removeAttr('disabled');
    }
};