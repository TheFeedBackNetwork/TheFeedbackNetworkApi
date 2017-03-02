import React, { Component, PropTypes } from 'react';
import * as d3 from "d3";
import { hex2rgba, rgb2Hex } from '../../utils/utilities';


class Waveform extends React.Component {

    componentDidMount() {
        this.setInitialState()        
        this.drawWaveform()
    }

    componentDidUpdate() {
        this.redrawWaveform()
        
    }

    componentWillUpdate() {

    }
    
    setInitialState() {
        const calculatedWidth = this.props.containerWidth - this.props.margin.left - this.props.margin.right
        const calculatedHeight = this.props.containerHeight - this.props.margin.top - this.props.margin.bottom;

        this.state = {width: calculatedWidth,
                        height: calculatedHeight}
    }

    setDimensions() {
        const calculatedWidth = this.props.containerWidth - this.props.margin.left - this.props.margin.right
        const calculatedHeight = this.props.containerHeight - this.props.margin.top - this.props.margin.bottom

        console.log(`${calculatedHeight} : ${calculatedWidth}`)

        this.setState({width: calculatedWidth,
                        height: calculatedHeight})
    }

    redrawWaveform() {
        console.log
        const context = d3.select(`#${this.props.id}`)

        context.remove()

        this.drawWaveform()
        this.animate(this.props.progress)
    }

    drawWaveform() {
        const { id, waveformData, seekedColour, unseekedColour, progress } = this.props
        const { width, height } = this.state
        //console.log('height')
        //console.log(height)
        const svg = this.setContext();

            svg.append('defs')
                .attr('id', -1)
        svg.selectAll("rect")
            .data(waveformData)
            
            .enter()
            .append("rect")
            .attr('id', (d, i) => `${id}-${i}`)
            .attr('rx',2)
            .attr('ry',2)
            .attr("x", (d, i) => (i*(width/waveformData.length)))
            .attr("y", (d) => height-((d/500)*height))
            .attr('fill', (d,i) => (i/waveformData.length*100 < progress ? seekedColour  : unseekedColour))
            .attr("width", width/waveformData.length - 1)
            .attr("height", (d) => {const a = height-((d/500)*height); return (height - d/500 -a+2 )});
    }

    setContext() {
        const { id } = this.props
        const { width, height } = this.state

        return d3.select(this.refs.bar)
                    .append('svg')
                    //.on('click', this.props.onClick(e))
                    .attr('width', width)
                    .attr('height', height)
                    .attr('class', 'waveform')
                    .attr('id', id)

    }

    animate(progress) {
        
        const { waveformData, seekedColour, unseekedColour, id } = this.props

        const normalizedProgress = progress/100 * waveformData.length

        if(normalizedProgress > 0 && normalizedProgress)
        {
            const from = hex2rgba(seekedColour, false)
            const to = hex2rgba(unseekedColour, false)
            const is = d3.interpolateRgb(from,to)(progress/100)
            const hexed = rgb2Hex(is)
            const floor = Math.floor(normalizedProgress)
            const context = d3.select(`[id="${id}-${floor}"]`)
                                .transition()
                                .attr('fill', hexed)
        }
    }

    render() {
        return (
            <div ref="bar" onClick={this.props.onClick}> </div>
        )
    }
}

Waveform.propTypes = {
    id: PropTypes.string.isRequired,
    waveformData: PropTypes.array.isRequired,
    progress: PropTypes.number,
    margin: PropTypes.object,
    unseekedColour: PropTypes.string,
    seekedColour: PropTypes.string,
    containerWidth: PropTypes.number,
    containerHeight: PropTypes.number,
    onClick: PropTypes.func
}

export default Waveform;