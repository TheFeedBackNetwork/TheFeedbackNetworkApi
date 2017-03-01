import React, { Component, PropTypes } from 'react';
import * as d3 from "d3";

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

const margin = {top: 20, right: 20, bottom: 30, left: 40};
const width = 960 - margin.left - margin.right;
const height = 500 - margin.top - margin.bottom;

//const x =  d3.scale.ordinal().rangeRoundBands([0, width], .05);

//const y = d3.scale.linear().range([height, 0]);

class Waveform extends Component {


    componentDidMount() {
        this.drawWaveform()
        console.log(this.props.progress)
    }

    drawWaveform() {
        const svg = this.setContext();
            svg.append('defs')
                .attr('id', 0)
        svg.selectAll("rect")
            .data(waveformData)
            
            .enter()
            .append("rect")
            .attr('id', (d, i) => i+1)
            .attr('rx',2)
            .attr('ry',2)
            .attr("x", (d, i) => (i*(width/waveformData.length)))
            .attr("y", (d) => height-d)
            .attr('fill', (d,i) => (i/waveformData.length*100 < this.props.progress ? '#000' : '#d1d6da'))
            //.attr('fill', '#d1d6da')
            .attr("width", width/waveformData.length - 1)
            .attr("height", (d) => d);
        //this.setBackGround(context);
        //this.setForeground(context);
    }

    setContext() {
        //const { width, height, id} = this.props
        const id = 'lol'
        return d3.select(this.refs.bar)
                    .append('svg')
                    .attr('width', width)
                    .attr('height', height)
                    .attr('class', 'waveform')
                    .attr('id', id)

    }

    componentDidUpdate() {
        console.log(this.props.progress)
        this.redrawWaveform()
    }

    redrawWaveform() {
        const context = d3.select(`#lol`)
        context.remove()
        this.drawWaveform()
    }

    setBackground(context) {

    }

    setForeground(context) {

    }

    waveform() {

    }

    render() {
        return (
            <div ref="bar"> </div>
        )
    }

}

Waveform.propTypes = {
 progress: PropTypes.number
}

export default Waveform;