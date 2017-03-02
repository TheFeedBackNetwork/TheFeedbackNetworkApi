import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import Waveform from '../components/Waveform';
import shortid from 'shortid';

const waveformData = [
    45,
    60,
    77,
    66,
    75,
    60,
    66,
    66,
    98,
    119,
    70,
    133,
    147,
    133,
    149,
    133,
    82,
    97,
    87,
    81,
    114,
    81,
    118,
    145,
    132,
    124,
    100,
    158,
    196,
    152,
    124,
    108,
]

class WaveformContainer extends React.Component {

    componentWillMount() {
        this.setState({seek:0, d: `b${shortid.generate()}`  })
    }

    seek(name, e) {
        this.setState({seek:parseInt(e.target.value)})
    }

    clicked(name, e) {
        //console.log(e.clientX);
        this.setState({seek:parseInt(e.clientX/900 * 100)})
    }

    render() {

        const m = {top: 20, right: 20, bottom: 30, left: 40};
        return (
            <div onClick={(e) => this.clicked('area', e)} style={{width: '900px'}}>
                <Waveform 
                    id = {this.state.d} 
                    progress = {this.state.seek}
                    waveformData = {waveformData}
                    margin = {m}
                    unseekedColour = {'#d1d6da'}
                    seekedColour= {'#f86a21'}
                    containerWidth = {900}
                    containerHeight = {500}
                 />
                <input value={this.state.seek} style={{width: '900px'}} type="range"  min="0" max="100" onChange={this.seek.bind(this, 'slider')}/>
            </div>
        )
    }
}

export default WaveformContainer;