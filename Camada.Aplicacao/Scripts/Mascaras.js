$("#telefone").inputmask("mask", { "mask": "(99) 9999-99999" });
$(".cpfTextField").inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true });
$("#cep").inputmask("mask", { "mask": "99999-999" });
//$(".currency").inputmask("mask", { "mask": "€ #.###.##9,99" }, { reverse: true });
//$(".currency").inputmask("mask", { "mask": "€ #.###9,99" }, { reverse: true, showSymbol: true, symbol: "€", decimal: ",", thousands: "." });
////$("#currency").maskMoney({ symbol: '€ ', thousands: '.', decimal: ',', symbolStay: true });
//$("#demo3").maskMoney({ allowZero: false, allowNegative: true, defaultZero: false });

//$(".destoyMoneyCurrenc").maskMoney('destroy');
//$(".moneyCurrency").maskMoney({ thousands: '.', decimal: ',', prefix: "€", allowNegative: true, defaultZero: false });

$("#nascimento").inputmask("mask", { "mask": "99/99/9999" });
$("#preco").inputmask("mask", { "mask": "999.999,99" }, { reverse: true });
$("#valor").inputmask("mask", { "mask": "#.##9,99" }, { reverse: true });
$("#ip").inputmask("mask", { "mask": "999.999.999.999" });
$(".dateTextField").inputmask("mask", { "mask": "99/99/9999", placeholder: "dd/mm/yyyy" });
$(".numeroProcessoTextField").inputmask("mask", { "mask": "9999999999" });
$(".anoMesDia").inputmask("mask", { "mask": "99" });
$(".mesAno").inputmask("mask", { "mask": "99/9999" });


//$("#phone").mask("(99) 9999-9999");
//$("#phone").mask("(999) 999-9999? x99999");
//$("#txtCPF").inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true });
//$("#txtCPF").mask("999.999.999-99");
//$("#tin").mask("99-9999999");
//$("#ssn").mask("999-99-9999");
//$("#product").mask("99/99/9999", { placeholder: " " });
//$("#product").mask("99/99/9999", { completed: function () { alert("You typed the following: " + this.val()); } });
