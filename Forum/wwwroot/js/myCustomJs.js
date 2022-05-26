
function ShowAnswers(commentId) {
    var x = document.getElementById("answers-" + commentId);
    var button = document.getElementById("answers-button-" + commentId);

    if (x.style.display === "none") {
        x.style.display = "block";
        button.innerHTML = "Hide Answers";
    } else {
        x.style.display = "none";
        button.innerHTML = "Show Answers";
    }
}

function confirmDeleteComment(commentId, isDeleteClicked) {

    var confirmDeleteSpan = "confirm-delete-span-" + commentId;
    var deleteSpan = "delete-" + commentId;
    var showAnswersButton = 'answers-button-' + commentId;
    var writeAnswerButton = 'write-answer-' + commentId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + showAnswersButton).hide();
        $('#' + writeAnswerButton).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + confirmDeleteSpan).hide();
        $('#' + showAnswersButton).show();
        $('#' + writeAnswerButton).show();
        $('#' + deleteSpan).show();
    }
}

function confirmDeleteAnswer(answerId, isDeleteClicked) {

    var confirmDeleteSpan = "confirm-delete-answer-span-" + answerId;
    var deleteSpan = "delete-answer-" + answerId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + confirmDeleteSpan).hide();
        $('#' + deleteSpan).show();
    }
}

