import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import Waveform from '../components/Waveform';
import shortid from 'shortid';

const waveformData = [
    1,
    40,
    48,
    38,
    37,
    112,
    148,
    146,
    131,
    137,
    142,
    155,
    150,
    127,
    193,
    241,
    220,
    225,
    249,
    230,
    223,
    218,
    241,
    178,
    102,
    112,
    146,
    146,
    155,
    149,
    107,
    221,
    247,
    291,
    285,
    283,
    284,
    289,
    277,
    280,
    238,
    268,
    300,
    284,
    285,
    282,
    288,
    283,
    277,
    276,
    258,
    124,
    171,
    215,
    162,
    203,
    205,
    239,
    156,
    161,
    216,
    183,
    186,
    174,
    191,
    195,
    197,
    117,
    152,
    213,
    210,
    159,
    209,
    158,
    177,
    158,
    172,
    187,
    182,
    280,
    305,
    285,
    292,
    287,
    297,
    292,
    284,
    261,
    280,
    304,
    282,
    284,
    288,
    298,
    293,
    284,
    276,
    104,
    1,
    0
]

class WaveformContainer extends React.Component {

    componentWillMount() {
        this.setState({seek:0, d: `b${shortid.generate()}`  })
    }

    seek(name, e) {
        this.setState({seek:parseInt(e.target.value)})
    }

    clicked(name, e) {
        this.setState({seek:parseInt(e.clientX/840 * 100)})
    }

    render() {

        const m = {top: 20, right: 20, bottom: 30, left: 40};
        return (
            <div  style={{width: '900px'}}>
                <Waveform 
                    id = {this.state.d} 
                    progress = {this.state.seek}
                    waveformData = {waveformData}
                    margin = {m}
                    unseekedColour = {'#d1d6da'}
                    seekedColour= {'#f86a21'}
                    containerWidth = {900}
                    containerHeight = {100}
                    onClick={(e) => this.clicked('area', e)}
                 />
            </div>
        )
    }
}

export default WaveformContainer;