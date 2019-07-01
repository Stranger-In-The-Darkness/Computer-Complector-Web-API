const properties =
{
    company: "Vendor",
    buildincharger: "Build-in charger",
    chargerpower: "Charger power",
    color: "Color",
    formfactor: "Formfactor",
    type: "Type",
    usb2ports: "USB 2 ports",
    usb3ports: "USB 3 ports",
    videocardmaxlength: "Videocard maximum length",
    additions: "Additions",
    connectortype: "CPU pin",
    ideamount: "IDE amout",
    motherboardconnector: "Motherboard pin",
    power: "Power",
    sataamount: "SATA amount",
    series: "Series",
    sertificate: "Sertificate 80 PLUS",
    videoconnectorsamount: "Video connectors",
    videocardconnector: "Videocard pin",
    connector: "",
    material: "Material",
    puspose: "Target",
    socket: "Socket",
    turnadj: "Turn adjustement",
    ventdiam: "Ventilator diameter",
    core: "Core",
    coresamount: "Amount of cores",
    deliverytype: "Delivery type",
    frequency: "Frequency",
    integratedgraphics: "Integrated graphics",
    overcloacking: "Overclocking",
    threadsamount: "Amount of threads",
    buffervolume: "Buffer volume",
    capacity: "Capacity",
    interface: "Interface",
    speed: "Speed",
    chipset: "Chipset",
    cpuCompany: "Fro CPU's",
    cpuPin: "CPU pin",
    maxmemory: "Maximum memory",
    memorychanelsamount: "Amount of memory chanels",
    memoryslotsamount: "Amount of memory slots",
    memorytype: "Memory type",
    pin: "Pin",
    rammaxfreq: "RAM maximum frequecny",
    slots: "Slots",
    cl: "CAS latency",
    freq: "Frequecny",
    modulesamount: "Amount of modules",
    volume: "Volume",
    celltype: "Memory cell type",
    connectors: "Connectors",
    family: "Family",
    length: "Length",
    memory: "Memory type",
    proccessor: "GPU",
    vram: "VRAM"
};

const tab = {
    selected: -1,
    names: ["body", "charger", "cooler", "cpu", "hdd", "motherboard", "ram", "ssd", "videocard"],
    change: function (type) {
        if (tab.selected === tab.names.indexOf(type)) return;
        tab.selected = tab.names.indexOf(type);
        tab.selected = tab.selected === -1 ? 0 : tab.selected;
        $("#filtersList").empty();
        $("#elementsList").empty();
        option.selected = {};
        property.get(type);
        element.get(type);
    }
};

const option = {
    selected: {},
    change: function (sender, index) {
        if (index >= 0) {
            var option = sender.children[index].value;
            option.selected[sender.name] = option;
        }
        else {
            console.error("Invalid option index");
        }
    }
};

const element = {
    all: {
        body: null,
        charger: null,
        cooler: null,
        cpu: null,
        hdd: null,
        motherboard: null,
        ram: null,
        ssd: null,
        videocard: null
    },
    selected: {
        body: null,
        charger: null,
        cooler: null,
        cpu: null,
        hdd: null,
        motherboard: null,
        ram: null,
        ssd: null,
        videocard: null,
        add: function (value) {
            var data = value.split("_");
            switch (data[0]) {
                case "body":
                    {
                        element.selected.body = element.all.body[data[1]]; console.log(element.all.body[data[1]]);
                        $("#selectedBody").empty();
                        $("#selectedBody").append(element.format(element.selected.body, data[0]));
                        $("#selectedBody").removeClass("inactive");
                    } break;
                case "charger":
                    {
                        element.selected.charger = element.all.charger[data[1]]; console.log(element.all.charger[data[1]]);
                        $("#selectedCharger").empty();
                        $("#selectedCharger").append(element.format(element.selected.charger, data[0]));
                        $("#selectedCharger").removeClass("inactive");
                    } break;
                case "cooler":
                    {
                        element.selected.cooler = element.all.cooler[data[1]]; console.log(element.all.cooler[data[1]]);
                        $("#selectedCooler").empty();
                        $("#selectedCooler").append(element.format(element.selected.cooler, data[0]));
                        $("#selectedCooler").removeClass("inactive");
                    } break;
                case "cpu":
                    {
                        element.selected.cpu = element.all.cpu[data[1]]; console.log(element.all.cpu[data[1]]);
                        $("#selectedCpu").empty();
                        $("#selectedCpu").append(element.format(element.selected.cpu, data[0]));
                        $("#selectedCpu").removeClass("inactive");
                    } break;
                case "hdd":
                    {
                        element.selected.hdd = element.all.hdd[data[1]]; console.log(element.all.hdd[data[1]]);
                        $("#selectedHdd").empty();
                        $("#selectedHdd").append(element.format(element.selected.hdd, data[0]));
                        $("#selectedHdd").removeClass("inactive");
                    } break;
                case "motherboard":
                    {
                        element.selected.motherboard = element.all.motherboard[data[1]]; console.log(element.all.motherboard[data[1]]);
                        $("#selectedMotherboard").empty();
                        $("#selectedMotherboard").append(element.format(element.selected.motherboard, data[0]));
                        $("#selectedMotherboard").removeClass("inactive");
                    } break;
                case "ram":
                    {
                        element.selected.ram = element.all.ram[data[1]]; console.log(element.all.ram[data[1]]);
                        $("#selectedRam").empty();
                        $("#selectedRam").append(element.format(element.selected.ram, data[0]));
                        $("#selectedRam").removeClass("inactive");
                    } break;
                case "ssd":
                    {
                        element.selected.ssd = element.all.ssd[data[1]]; console.log(element.all.ssd[data[1]]);
                        $("#selectedSsd").empty();
                        $("#selectedSsd").append(element.format(element.selected.ssd, data[0]));
                        $("#selectedSsd").removeClass("inactive");
                    } break;
                case "videocard":
                    {
                        element.selected.videocard = element.all.videocard[data[1]]; console.log(element.all.videocard[data[1]]);
                        $("#selectedVideocard").empty();
                        $("#selectedVideocard").append(element.format(element.selected.videocard, data[0]));
                        $("#selectedVideocard").removeClass("inactive");
                    } break;
            }
        },
        remove: function (value) {
            var data = value.split("_");
            switch (data[0]) {
                case "body":
                    {
                        element.selected.body = null;
                        $("#selectedBody").empty();
                        $("#selectedBody").addClass("inactive");
                    } break;
                case "charger":
                    {
                        element.selected.charger = null;
                        $("#selectedCharger").empty();
                        $("#selectedCharger").addClass("inactive");
                    } break;
                case "cooler":
                    {
                        element.selected.cooler = null;
                        $("#selectedCooler").empty();
                        $("#selectedCooler").addClass("inactive");
                    } break;
                case "cpu":
                    {
                        element.selected.cpu = null;
                        $("#selectedCpu").empty();
                        $("#selectedCpu").addClass("inactive");
                    } break;
                case "hdd":
                    {
                        element.selected.hdd = null;
                        $("#selectedHdd").empty();
                        $("#selectedHdd").addClass("inactive");
                    } break;
                case "motherboard":
                    {
                        element.selected.motherboard = null;
                        $("#selectedMotherboard").empty();
                        $("#selectedMotherboard").addClass("inactive");
                    } break;
                case "ram":
                    {
                        element.selected.ram = null;
                        $("#selectedRam").empty();
                        $("#selectedRam").addClass("inactive");
                    } break;
                case "ssd":
                    {
                        element.selected.ssd = null;
                        $("#selectedSsd").empty();
                        $("#selectedSsd").addClass("inactive");
                    } break;
                case "videocard":
                    {
                        element.selected.videocard = null;
                        $("#selectedVideocard").empty();
                        $("#selectedVideocard").addClass("inactive");
                    } break;
            }
        }
    },
    get: function (type) {
        if (type !== null && type != "undefined") {
            if (typeof (type) === "string") {
                switch (type) {
                    case "body":
                    case "charger":
                    case "cooler":
                    case "cpu":
                    case "hdd":
                    case "motherboard":
                    case "ram":
                    case "ssd":
                    case "videocard":
                        {
                            var url = `api/components/${type}?`;
                            var q = [];
                            $.each(option.selected, (index, value) => {
                                if (index != "change") {
                                    q.push(`${index}=${value}`);
                                }
                            });
                            $.each(element.selected, (index, value) => {
                                if (tab.names.indexOf(index) !== 0) {
                                    if (value !== null) {
                                        q.push(`selected-${index}=${value.id}`);
                                    }
                                }
                            });
                            url += q.join("&");

                            $.ajax({
                                url: url,
                                type: "GET",
                                success: function (res) {
                                    $.each(res, function (index, value) {
                                        if (element.all[type] === null) {
                                            element.all[type] = {};
                                        }
                                        element.all[type][value.id] = value;
                                    });
                                    $("#elementsList").empty();
                                    $.each(res, (index, value) => {
                                        $("#elementsList").append(element.format(value, type));
                                    });
                                    $('.element-select-btn').click(
                                        function () {
                                            var id = $(this).attr('id');
                                            if ($(this).hasClass("remove-btn")) {
                                                element.selected.remove(id);
                                                $(this).parent().removeClass('selected');
                                                $(this).removeClass("remove-btn");
                                                $(this).val("+");
                                            }
                                            else {
                                                element.selected.add(id);
                                                $(this).parent().addClass('selected');
                                                $(this).addClass("remove-btn");
                                                $(this).val("X");
                                            }
                                        });
                                },
                                error: function (err) {
                                    console.log(err);
                                }
                            });
                            break;
                        }
                }
            }
        }
    },
    format: function (value, type) {
        console.log(value);
        console.log(`Type = ${type}`);
        var inner = "";
        if (element.selected[type] !== null && value.id === element.selected[type].id) {
            inner = "<li class='element selected'><table class='elementInfo' cellspacing='0' cellpadding='0'>";
        }
        else {
            inner = "<li class='element'><table class='elementInfo' cellspacing='0' cellpadding='0'>";
        }
        $.each(value, (index, value) => {
            switch (index) {
                case "id": break;
                case "title":
                    {
                        inner += `<th colspan="2"><div class="elementHeader">${value}</div><hr/></th>`;
                    }
                    break;
                default: {
                    inner += `<tr><td class="elementProperty">${properties[index.toLowerCase()]}</td><td class="elementProperty">${value}</td>`;
                }
                    break;
            }
        });
        if (element.selected[type] !== null && value.id === element.selected[type].id) {
            inner += `</table><input type="button" class="element-select-btn remove-btn" id="${type}_${value.id}" value="X" /></li>`;
        }
        else {
            inner += `</table><input type="button" class="element-select-btn" id="${type}_${value.id}" value="+" /></li>`;
        }
        return inner;
    }
};

const property = {
    get: function (type) {
        if (type !== null && type != "undefined") {
            if (typeof (type) === "string") {
                var url = "api/components/";
                switch (type) {
                    case "body":
                    case "charger":
                    case "cooler":
                    case "cpu":
                    case "hdd":
                    case "motherboard":
                    case "ram":
                    case "ssd":
                    case "videocard":
                        {
                            $.ajax({
                                url: `${url}/${type}/properties`,
                                type: "GET",
                                success: function (res) {
                                    $("#filtersList").empty();
                                    $.each(res, (index, value) => {
                                        var inner = `<li><div class="filter"><div class="filterHeader">${index}</div><select size="1" name=${index.toLowerCase().split(" ").join("-")} class="filterOptions" onchange="selection.options.change(this, this.selectedIndex)">`;
                                        $.each(value.item3, (index, value) => {
                                            inner += `<option>${value}</option>`;
                                        });
                                        inner += "</select></div></li>";
                                        $("#filtersList").append(inner);
                                    });
                                },
                                error: function (err) {
                                    console.log(err);
                                }
                            });
                            break;
                        }
                }
            }
        }
    }
};

tab.change("body");