$(".calculator").each(function () {
    var calculator = $(this);
    var calculatorInput = calculator.find(".calculatorInput");

    calculator.find(".calculatorButton").each(function () {
        var button = $(this);

        button.click(function () {
            var operation = button.attr("data-operation");
            var input = button.attr("data-input");
            var currentValue = calculatorInput.attr("value");
            var newValue = null;

            if (operation !== undefined && operation !== null) {
                switch (operation) {
                    case "del":
                        newValue = currentValue.substr(0, currentValue.length - 1);
                        break;
                    case "CE":
                        newValue = "";
                        $(".calculatorResult").attr("value", "");
                        break;
                    case "equals":
                        CalculateResult(calculator);
                        break;
                }
            } else if (input !== undefined && input !== null) {
                if (input === ".") {
                    newValue = currentValue + ".";
                } else {
                    var isDecimalNum = false;
                    var i = currentValue.length - 1;

                    if (["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"].indexOf(input) !== -1) {
                        while (i >= 0) {
                            if (currentValue.charAt(i) === " ") {
                                break;
                            } else if (currentValue.charAt(i) === ".") {
                                isDecimalNum = true;
                                break;
                            }

                            i--;
                        }
                    }

                    if (isDecimalNum) {
                        newValue = currentValue + input;
                    } else {
                        newValue = currentValue + " " + input;
                    } 
                }
            }
            
            if (newValue !== null) {
                calculatorInput.attr("value", newValue);
            }
        });
    });
});

function CalculateResult(calculator) {
    var currentValue = calculator.find(".calculatorInput").attr("value");
    var urlAddition = "Input=" + currentValue;

    $(calculator.find(".calculatorParameterHolder")).each(function (i, item) {
        urlAddition += "&Parameters[" + i + "].Key=" + $(item).find(".calculatorParameterName").attr("value");
        urlAddition += "&Parameters[" + i + "].Value=" + $(item).find(".calculatorParameterEquation").attr("value");
    });

    urlAddition = urlAddition.replace("+", "%2B");

    $.ajax({
        url: "Calculator/Calculate?" + urlAddition,
        dataType: "json",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

        before: function() {
            calculator.find(".calculatorLoading").show();
        },

        success: function(data) {
            if (data.Error !== null) {
                calculator.find(".calculatorResult").attr("value", data.Error);
            } else if (data.Result !== null) {
                calculator.find(".calculatorResult").attr("value", data.Result);
            }
        },

        complete: function () {
            calculator.find(".calculatorLoading").hide();
        },
    });
}