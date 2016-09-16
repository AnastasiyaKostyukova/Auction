function makeRate() {
    var self = this;
    
    self.showMakeRateForm = function() {
        $('#makeRateForm').removeClass('hidden');
        $('#myRatesLotsButton').addClass('hidden');
    }
}