$(".calculator").each(function () {
    var calculator = $(this);
    var calculatorInput = calculator.find(".calculatorInput");

    calculator.find(".calculatorButton").each(function () {
        var button = $(this);

        button.click(function () {
            ButtonClick(button, calculatorInput, calculator);
        });
    });

    var paramTables = calculator.find(".parameterTable");

    paramTables.each(function () {
        var paramTable = $(this);

        paramTable.find(".calculatorParameterHolder").each(function () {
            var holder = $(this);

            holder.find(".calculatorParameterName").bind("input", function () {
                holder.find(".calculatorButton").attr("data-input", holder.find(".calculatorParameterName").val());
                holder.find(".calculatorButton").text(holder.find(".calculatorParameterName").val());
            });
        });

        paramTable.find(".addParamButton").click(function () {
            var addButton = $(this);

            var tr = $("<tr>");
            tr.addClass("calculatorParameterHolder")

            var td1 = $("<td>");
            var td2 = $("<td>");
            var td3 = $("<td>");

            var button = $("<button>");
            button.addClass("btn btn-block calculatorButton");
            button.text("Add");
            button.click(function () {
                ButtonClick(button, calculatorInput, calculator);
            });

            var inputName = $("<input>");
            inputName.addClass("calculatorParameter calculatorParameterName");
            inputName.attr("type", "text");

            var inputEquation = $("<input>");
            inputEquation.addClass("calculatorParameter calculatorParameterEquation");
            inputEquation.attr("type", "text");

            tr.append(td1);
            tr.append(td2);
            tr.append(td3);

            td1.append(button);
            td2.append(inputName);
            td3.append(inputEquation);

            tr.insertBefore(addButton.parent().parent());

            tr.bind("input", function () {
                tr.find(".calculatorButton").attr("data-input", tr.find(".calculatorParameterName").val());
            });
        });
    });
});

function ButtonClick(button, calculatorInput, calculator) {
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
            var numbers = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
            if (numbers.indexOf(input) === -1) {
                newValue = currentValue + " " + input;
            } else {
                if (numbers.indexOf(currentValue.charAt(currentValue.length - 1)) === -1) {
                    newValue = currentValue + " " + input;
                } else {
                    newValue = currentValue + input;
                }
            }
        }
    }

    if (newValue !== null) {
        calculatorInput.attr("value", newValue);
    }
}

function CalculateResult(calculator) {
    var currentValue = calculator.find(".calculatorInput").val();
    var urlAddition = "Input=" + currentValue;

    $(calculator.find(".calculatorParameterHolder")).each(function (i, item) {
        urlAddition += "&Parameters[" + i + "].Key=" + $(item).find(".calculatorParameterName").val();
        urlAddition += "&Parameters[" + i + "].Value=" + $(item).find(".calculatorParameterEquation").val();
    });

    urlAddition = urlAddition.replace("+", "%2B");

    $.ajax({
        url: "Calculator/Calculate?" + urlAddition,
        dataType: "json",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

        before: function () {
            calculator.find(".calculatorLoading").show();
        },

        success: function (data) {
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