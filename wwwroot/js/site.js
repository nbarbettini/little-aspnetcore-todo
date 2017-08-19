// Write your JavaScript code.

$(document).ready(function() {
    
        // Wire up all of the checkboxes to run markCompleted()
        $('.done-checkbox').on('click', function(e) {
            markCompleted(e.target);
        });
    
    });
    
    function markCompleted(checkbox) {
        checkbox.disabled = true;
    
        $.post('/Todo/MarkDone', { id: checkbox.name }, function() {
            var row = checkbox.parentElement.parentElement;
            $(row).addClass('done');
        });
    }
    